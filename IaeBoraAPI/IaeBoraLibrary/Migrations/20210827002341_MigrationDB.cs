using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IaeBoraLibrary.Migrations
{
    public partial class MigrationDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    GoogleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.GoogleId);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Food = table.Column<int>(type: "int", nullable: false),
                    Musics = table.Column<int>(type: "int", nullable: false),
                    Religion = table.Column<int>(type: "int", nullable: false),
                    Sports = table.Column<int>(type: "int", nullable: false),
                    Teams = table.Column<int>(type: "int", nullable: false),
                    HaveChildren = table.Column<int>(type: "int", nullable: false),
                    UserGoogleId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Users_UserGoogleId",
                        column: x => x.UserGoogleId,
                        principalTable: "Users",
                        principalColumn: "GoogleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserGoogleId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_Users_UserGoogleId",
                        column: x => x.UserGoogleId,
                        principalTable: "Users",
                        principalColumn: "GoogleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserGoogleId",
                table: "Answers",
                column: "UserGoogleId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_UserGoogleId",
                table: "Routes",
                column: "UserGoogleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
