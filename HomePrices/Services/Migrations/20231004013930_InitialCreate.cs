using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomePrices.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Homes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    YearBuilt = table.Column<int>(type: "INTEGER", nullable: true),
                    SquareFeet = table.Column<int>(type: "INTEGER", nullable: true),
                    PriceDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PriceQuarter = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PriceWeek = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeRegion",
                columns: table => new
                {
                    HomeListId = table.Column<int>(type: "INTEGER", nullable: false),
                    RegionListId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeRegion", x => new { x.HomeListId, x.RegionListId });
                    table.ForeignKey(
                        name: "FK_HomeRegion_Homes_HomeListId",
                        column: x => x.HomeListId,
                        principalTable: "Homes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeRegion_Regions_RegionListId",
                        column: x => x.RegionListId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeRegion_RegionListId",
                table: "HomeRegion",
                column: "RegionListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeRegion");

            migrationBuilder.DropTable(
                name: "Homes");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
