using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AUPS_Backend.Migrations
{
    /// <inheritdoc />
    public partial class object_of_labor_material : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "object_of_labor_material",
                columns: table => new
                {
                    object_of_labor_material_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    object_of_labor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    material_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__object_o__7D4DF5F1514A3214", x => x.object_of_labor_material_id);
                    table.ForeignKey(
                        name: "FK__object_of__objec__544C7345",
                        column: x => x.object_of_labor_id,
                        principalTable: "object_of_labor",
                        principalColumn: "object_of_labor_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__object_of__techn__55409A43",
                        column: x => x.material_id,
                        principalTable: "material",
                        principalColumn: "material_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_object_of_labor_material_material_id",
                table: "object_of_labor_material",
                column: "material_id");

            migrationBuilder.CreateIndex(
                name: "IX_object_of_labor_material_object_of_labor_id",
                table: "object_of_labor_material",
                column: "object_of_labor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "object_of_labor_material");
        }
    }
}
