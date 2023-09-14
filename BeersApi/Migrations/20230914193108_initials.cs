using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeersApi.Migrations
{
    /// <inheritdoc />
    public partial class initials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "beer_types",
                columns: table => new
                {
                    beer_types_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    status = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_beer_types", x => x.beer_types_id);
                });

            migrationBuilder.CreateTable(
                name: "beers",
                columns: table => new
                {
                    beers_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    alcohol_rate = table.Column<int>(type: "INTEGER", nullable: false),
                    price = table.Column<int>(type: "INTEGER", nullable: false),
                    beer_type_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    status = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_beers", x => x.beers_id);
                    table.ForeignKey(
                        name: "FK_beers_beer_types_beer_type_id",
                        column: x => x.beer_type_id,
                        principalTable: "beer_types",
                        principalColumn: "beer_types_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_beers_beer_type_id",
                table: "beers",
                column: "beer_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "beers");

            migrationBuilder.DropTable(
                name: "beer_types");
        }
    }
}
