using MessagingMicroservice.Database.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessagingMicroservice.Database.Models
{
    public class TextSendRequests
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RowId { get; set; }

        [Required]
        public long ChatId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public ProcessingStatus MessageProcessingStatus { get; set; } = ProcessingStatus.NotScheduled;

        [Required]
        public int FailedProcessingAttempts { get; set; } = 0;
    }
}