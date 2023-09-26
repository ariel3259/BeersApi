using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeersApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "drink_types",
                columns: table => new
                {
                    drink_types_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drink_types", x => x.drink_types_id);
                });

            migrationBuilder.CreateTable(
                name: "drinks",
                columns: table => new
                {
                    drinks_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    alcohol_rate = table.Column<double>(type: "float", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    drink_type_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drinks", x => x.drinks_id);
                    table.ForeignKey(
                        name: "FK_drinks_drink_types_drink_type_id",
                        column: x => x.drink_type_id,
                        principalTable: "drink_types",
                        principalColumn: "drink_types_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_drinks_drink_type_id",
                table: "drinks",
                column: "drink_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "drinks");

            migrationBuilder.DropTable(
                name: "drink_types");
        }
    }
}
