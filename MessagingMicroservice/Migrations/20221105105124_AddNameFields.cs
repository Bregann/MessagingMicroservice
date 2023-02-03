using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessagingMicroservice.Migrations
{
    public partial class AddNameFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromEmailAddressName",
                table: "EmailSendRequest",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToEmailAddressName",
                table: "EmailSendRequest",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromEmailAddressName",
                table: "EmailSendRequest");

            migrationBuilder.DropColumn(
                name: "ToEmailAddressName",
                table: "EmailSendRequest");
        }
    }
}