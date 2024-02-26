using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessagingMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTextChatIdToLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ChatId",
                table: "TextSendRequests",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ChatId",
                table: "TextSendRequests",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
