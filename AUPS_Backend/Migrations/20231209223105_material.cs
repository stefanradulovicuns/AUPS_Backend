using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AUPS_Backend.Migrations
{
    /// <inheritdoc />
    public partial class material : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "material",
                columns: table => new
                {
                    material_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    material_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    stock_quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__material__734FE6BF2BC6D112", x => x.material_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "material");
        }
    }
}
