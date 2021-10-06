using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IaeBoraLibrary.Migrations
{
    public partial class MigrationDB_V0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RouteDateAndTime",
                table: "Answers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RouteDateAndTime",
                table: "Answers");
        }
    }
}
