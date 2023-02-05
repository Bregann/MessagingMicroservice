using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessagingMicroservice.Database.Models
{
    public class Config
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RowId { get; set; }

        [Required]
        public string? SendGridApiKey { get; set; }

        [Required]
        public string? HFConnectionString { get; set; }

        [Required]
        public string HangfireUsername { get; set; }

        [Required]
        public string HangfirePassword { get; set; }

        [Required]
        public string? ProjectMonitorKey { get; set; }

        [Required]
        public string? TelegramBotApiKey { get; set; }

        [Required]
        public string? ApiAuthKey { get; set; }
    }
}