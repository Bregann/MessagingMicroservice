using MessagingMicroservice.Database.Context;
using Serilog;

namespace MessagingMicroservice
{
    public class AppConfig
    {
        public static string ConnectionString { get; private set; } = Environment.GetEnvironmentVariable("MMSConnString");

        public static string SendGridApiKey { get; private set; } = "";
        public static string HFConnectionString { get; private set; } = "";
        public static string ProjectMonitorKey { get; private set; } = "";
        public static string ApiAuthKey { get; private set; } = "";
        public static string TelegramBotApiKey { get; private set; } = "";

        public static void LoadConfig()
        {
            using (var context = new DatabaseContext())
            {
                var config = context.Config.First();

                SendGridApiKey = config.SendGridApiKey;
                HFConnectionString = config.HFConnectionString;
                ProjectMonitorKey = config.ProjectMonitorKey;
                TelegramBotApiKey = config.TelegramBotApiKey;
                ApiAuthKey = config.ApiAuthKey;
                Log.Information("Config loaded");
            }
        }
    }
}