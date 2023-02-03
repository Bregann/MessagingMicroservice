using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessagingMicroservice.Migrations
{
    public partial class RemoveSubjectCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "EmailSendRequest");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "EmailSendRequest",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}