using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessagingMicroservice.Migrations
{
    public partial class AddApiAuthKeyField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiAuthKey",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Config",
                keyColumn: "RowId",
                keyValue: 1,
                column: "ApiAuthKey",
                value: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiAuthKey",
                table: "Config");
        }
    }
}