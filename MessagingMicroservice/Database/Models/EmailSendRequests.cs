using MessagingMicroservice.Database.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessagingMicroservice.Database.Models
{
    public class EmailSendRequests
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RowId { get; set; }

        [Required]
        public string? ToEmailAddress { get; set; }

        [Required]
        public string? ToEmailAddressName { get; set; }

        [Required]
        public string? FromEmailAddress { get; set; }

        [Required]
        public string? FromEmailAddressName { get; set; }

        [Required]
        public string? Content { get; set; }

        [Required]
        public string? TemplateId { get; set; }

        [Required]
        public ProcessingStatus EmailProcessingStatus { get; set; } = ProcessingStatus.NotScheduled;

        [Required]
        public int FailedProcessingAttempts { get; set; } = 0;
    }
}