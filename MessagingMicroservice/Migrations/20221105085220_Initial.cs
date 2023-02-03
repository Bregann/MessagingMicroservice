using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MessagingMicroservice.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Config",
                columns: table => new
                {
                    RowId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SendGridApiKey = table.Column<string>(type: "text", nullable: false),
                    HFConnectionString = table.Column<string>(type: "text", nullable: false),
                    ProjectMonitorKey = table.Column<string>(type: "text", nullable: false),
                    EmailApiKey = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config", x => x.RowId);
                });

            migrationBuilder.CreateTable(
                name: "EmailSendRequest",
                columns: table => new
                {
                    RowId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ToEmailAddress = table.Column<string>(type: "text", nullable: false),
                    FromEmailAddress = table.Column<string>(type: "text", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    TemplateId = table.Column<string>(type: "text", nullable: false),
                    EmailProcessingStatus = table.Column<int>(type: "integer", nullable: false),
                    FailedProcessingAttempts = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSendRequest", x => x.RowId);
                });

            migrationBuilder.InsertData(
                table: "Config",
                columns: new[] { "RowId", "EmailApiKey", "HFConnectionString", "ProjectMonitorKey", "SendGridApiKey" },
                values: new object[] { 1, "", "", "", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Config");

            migrationBuilder.DropTable(
                name: "EmailSendRequest");
        }
    }
}