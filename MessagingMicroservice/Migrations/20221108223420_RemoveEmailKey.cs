using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessagingMicroservice.Migrations
{
    public partial class RemoveEmailKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailApiKey",
                table: "Config");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailApiKey",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Config",
                keyColumn: "RowId",
                keyValue: 1,
                column: "EmailApiKey",
                value: "");
        }
    }
}