﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessagingMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class AddHangfireDashboardConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HangfirePassword",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HangfireUsername",
                table: "Config",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Config",
                keyColumn: "RowId",
                keyValue: 1,
                columns: new[] { "HangfirePassword", "HangfireUsername" },
                values: new object[] { "", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HangfirePassword",
                table: "Config");

            migrationBuilder.DropColumn(
                name: "HangfireUsername",
                table: "Config");
        }
    }
}
