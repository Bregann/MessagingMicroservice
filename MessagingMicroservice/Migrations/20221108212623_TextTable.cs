using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MessagingMicroservice.Migrations
{
    public partial class TextTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TelegramBotApiKey",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TextSendRequests",
                columns: table => new
                {
                    RowId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChatId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    MessageProcessingStatus = table.Column<int>(type: "integer", nullable: false),
                    FailedProcessingAttempts = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextSendRequests", x => x.RowId);
                });

            migrationBuilder.UpdateData(
                table: "Config",
                keyColumn: "RowId",
                keyValue: 1,
                column: "TelegramBotApiKey",
                value: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextSendRequests");

            migrationBuilder.DropColumn(
                name: "TelegramBotApiKey",
                table: "Config");
        }
    }
}