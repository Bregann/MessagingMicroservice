using MessagingMicroservice.Database.Context;
using MessagingMicroservice.Database.Models;
using MessagingMicroservice.Dtos.Request;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MessagingMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendController : ControllerBase
    {
        [HttpPost("ScheduleEmail")]
        public ActionResult ScheduleEmail([FromBody] ScheduleEmailRequestDto requestDto)
        {
            //Make sure it's a valid email
            if (!requestDto.ToEmailAddress.Contains('@') || !requestDto.FromEmailAddress.Contains('@'))
            {
                Log.Information($"[Bregan Microservice] ScheduleEmail - poorly formatted email");
                return BadRequest();
            }

            string apiKey = Request.Headers.Authorization;

            if (apiKey == null || AppConfig.ApiAuthKey != apiKey)
            {
                Log.Information($"[Bregan Microservice] ScheduleEmail - request api key is not set or doesn't match");
                return BadRequest();
            }

            using (var context = new DatabaseContext())
            {
                context.EmailSendRequest.Add(new EmailSendRequests
                {
                    Content = requestDto.Content,
                    TemplateId = requestDto.TemplateId,
                    ToEmailAddress = requestDto.ToEmailAddress,
                    ToEmailAddressName = requestDto.ToEmailAddressName,
                    FromEmailAddress = requestDto.FromEmailAddress,
                    FromEmailAddressName = requestDto.FromEmailAddressName
                });

                context.SaveChanges();
            }

            return Ok();
        }

        [HttpPost("ScheduleText")]
        public IActionResult ScheduleText([FromBody] ScheduleTextRequestDto requestDto)
        {
            string apiKey = Request.Headers.Authorization;

            if (apiKey == null || AppConfig.ApiAuthKey != apiKey)
            {
                Log.Information($"[Bregan Microservice] ScheduleText - request api key is not set or doesn't match");
                return BadRequest();
            }

            using (var context = new DatabaseContext())
            {
                context.TextSendRequests.Add(new TextSendRequests
                {
                    ChatId = requestDto.ChatId,
                    Content = requestDto.Content
                });

                context.SaveChanges();
            }

            return Ok();
        }
    }
}