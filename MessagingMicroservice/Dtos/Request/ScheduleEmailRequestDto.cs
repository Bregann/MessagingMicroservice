namespace MessagingMicroservice.Dtos.Request
{
    public class ScheduleEmailRequestDto
    {
        public string ToEmailAddress { get; set; }
        public string ToEmailAddressName { get; set; }
        public string FromEmailAddress { get; set; }
        public string FromEmailAddressName { get; set; }
        public string Content { get; set; }
        public string TemplateId { get; set; }
    }
}