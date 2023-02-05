using MessagingMicroservice.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace MessagingMicroservice.Database.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Config> Config { get; set; }
        public DbSet<EmailSendRequests> EmailSendRequest { get; set; }
        public DbSet<TextSendRequests> TextSendRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Config>().HasData(
                new Config
                {
                    RowId = 1,
                    HFConnectionString = "",
                    ProjectMonitorKey = "",
                    SendGridApiKey = "",
                    TelegramBotApiKey = "",
                    ApiAuthKey = "",
                    HangfireUsername = "",
                    HangfirePassword = ""
                });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(AppConfig.ConnectionString);
    }
}