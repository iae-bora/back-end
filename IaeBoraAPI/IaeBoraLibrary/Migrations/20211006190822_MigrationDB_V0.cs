using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IaeBoraLibrary.Migrations
{
    public partial class MigrationDB_V0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    City = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    RestaurantCategory = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.Id);
                });

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
                name: "OpeningHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Open = table.Column<bool>(type: "bit", nullable: false),
                    StartHour = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndHour = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlaceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpeningHours_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Food = table.Column<int>(type: "int", nullable: false),
                    Musics = table.Column<int>(type: "int", nullable: false),
                    Movies = table.Column<int>(type: "int", nullable: false),
                    Religion = table.Column<int>(type: "int", nullable: false),
                    Sports = table.Column<int>(type: "int", nullable: false),
                    Teams = table.Column<int>(type: "int", nullable: false),
                    HaveChildren = table.Column<int>(type: "int", nullable: false),
                    UserAge = table.Column<int>(type: "int", nullable: false),
                    PlacesCount = table.Column<int>(type: "int", nullable: false),
                    RouteDateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "TouristPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpeningHoursId = table.Column<int>(type: "int", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false),
                    DistanceFromOrigin = table.Column<double>(type: "float", nullable: false),
                    StartHour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndHour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TouristPoints_OpeningHours_OpeningHoursId",
                        column: x => x.OpeningHoursId,
                        principalTable: "OpeningHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TouristPoints_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserGoogleId",
                table: "Answers",
                column: "UserGoogleId");

            migrationBuilder.CreateIndex(
                name: "IX_OpeningHours_PlaceId",
                table: "OpeningHours",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_UserGoogleId",
                table: "Routes",
                column: "UserGoogleId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristPoints_OpeningHoursId",
                table: "TouristPoints",
                column: "OpeningHoursId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristPoints_RouteId",
                table: "TouristPoints",
                column: "RouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "TouristPoints");

            migrationBuilder.DropTable(
                name: "OpeningHours");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
