using MessagingMicroservice;
using MessagingMicroservice.Database.Context;
using MessagingMicroservice.Database.Models.Enums;
using Hangfire;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EmailMicroservice
{
    public class HangfireJobs
    {
        public static void SetupHangfireJobs()
        {
            RecurringJob.AddOrUpdate("QueueEmailsToSend", () => QueueEmailsToSend(), "*/20 * * * * *");
            RecurringJob.AddOrUpdate("QueueTextsToSend", () => QueueTextsToSend(), "*/20 * * * * *");
        }

        public static void QueueEmailsToSend()
        {
            var maxRequests = 5;
            using (var context = new DatabaseContext())
            {
                var requestsToProcess = context.EmailSendRequest.Where(x => x.EmailProcessingStatus == ProcessingStatus.NotScheduled).Count();

                if (requestsToProcess == 0)
                {
                    return;
                }

                Log.Information($"[Email Service] There are {requestsToProcess} emails to send");

                var activeRequests = context.EmailSendRequest.Where(x => x.EmailProcessingStatus == ProcessingStatus.Scheduled || x.EmailProcessingStatus == ProcessingStatus.BeingProcessed).Count();
                var requestsToSend = maxRequests - activeRequests;

                Log.Information($"[Email Service] {requestsToSend} emails ready to send");

                var rowsToSchedule = context.EmailSendRequest.Where(x => x.EmailProcessingStatus == ProcessingStatus.NotScheduled)
                    .Select(x => x.RowId)
                    .Take(requestsToSend)
                    .ToList();

                foreach (var id in rowsToSchedule)
                {
                    BackgroundJob.Enqueue(() => SendEmail(id));
                    context.EmailSendRequest.First(x => x.RowId == id).EmailProcessingStatus = ProcessingStatus.Scheduled;
                    context.SaveChanges();

                    Log.Information($"[Email Service] email id {id} scheduled to send");
                }
            }
        }

        public static void QueueTextsToSend()
        {
            var maxRequests = 5;

            using (var context = new DatabaseContext())
            {
                var requestsToProcess = context.TextSendRequests.Where(x => x.MessageProcessingStatus == ProcessingStatus.NotScheduled).Count();

                if (requestsToProcess == 0)
                {
                    return;
                }

                Log.Information($"[Text Service] There are {requestsToProcess} texts to send");

                var activeRequests = context.TextSendRequests.Where(x => x.MessageProcessingStatus == ProcessingStatus.Scheduled || x.MessageProcessingStatus == ProcessingStatus.BeingProcessed).Count();
                var requestsToSend = maxRequests - activeRequests;

                Log.Information($"[Text Service] {requestsToSend} text ready to send");

                var rowsToSchedule = context.TextSendRequests.Where(x => x.MessageProcessingStatus == ProcessingStatus.NotScheduled)
                    .Select(x => x.RowId)
                    .Take(requestsToSend)
                    .ToList();

                foreach (var id in rowsToSchedule)
                {
                    BackgroundJob.Enqueue(() => SendText(id));
                    context.TextSendRequests.First(x => x.RowId == id).MessageProcessingStatus = ProcessingStatus.Scheduled;
                    context.SaveChanges();

                    Log.Information($"[Text Service] text id {id} scheduled to send");
                }
            }
        }

        public static async Task SendEmail(int id)
        {
            using (var context = new DatabaseContext())
            {
                var emailData = context.EmailSendRequest.First(x => x.RowId == id);

                try
                {
                    var client = new SendGridClient(AppConfig.SendGridApiKey);
                    var from = new EmailAddress(emailData.FromEmailAddress, emailData.FromEmailAddressName);
                    var to = new EmailAddress(emailData.ToEmailAddress, emailData.ToEmailAddressName);

                    var msg = MailHelper.CreateSingleTemplateEmail(from, to, emailData.TemplateId, JsonConvert.DeserializeObject(emailData.Content));
                    var response = await client.SendEmailAsync(msg);

                    if (response.IsSuccessStatusCode)
                    {
                        emailData.EmailProcessingStatus = ProcessingStatus.Processed;
                        context.SaveChanges();

                        Log.Information($"[Email Service] email id {id} processed");
                    }
                    else
                    {
                        emailData.EmailProcessingStatus = ProcessingStatus.Errored;
                        emailData.FailedProcessingAttempts = emailData.FailedProcessingAttempts + 1;
                        context.SaveChanges();

                        Log.Warning($"[Email Service] email id {id} errored at SendGrid");
                    }
                }
                catch (Exception e)
                {
                    emailData.EmailProcessingStatus = ProcessingStatus.Errored;
                    emailData.FailedProcessingAttempts = emailData.FailedProcessingAttempts + 1;
                    context.SaveChanges();

                    Log.Error($"[Email Service] email id {id} errored - {e}");
                    return;
                }
            }
        }

        public static async Task SendText(int id)
        {
            using (var context = new DatabaseContext())
            {
                var textData = context.TextSendRequests.First(x => x.RowId == id);

                try
                {
                    var botClient = new TelegramBotClient(AppConfig.TelegramBotApiKey);

                    using var cts = new CancellationTokenSource();

                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: textData.ChatId,
                        text: textData.Content);

                    cts.Cancel();

                    textData.MessageProcessingStatus = ProcessingStatus.Processed;
                    context.SaveChanges();

                    Log.Information($"[Text Service] text id {id} processed");
                }
                catch (Exception e)
                {
                    textData.MessageProcessingStatus = ProcessingStatus.Errored;
                    textData.FailedProcessingAttempts = textData.FailedProcessingAttempts + 1;
                    context.SaveChanges();

                    Log.Error($"[Text Service] text id {id} errored - {e}");
                    return;
                }
            }
        }
    }
}