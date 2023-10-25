using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AUPS_Backend.Migrations
{
    /// <inheritdoc />
    public partial class order_of_execution_moved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_of_execution",
                table: "technological_procedure");

            migrationBuilder.AddColumn<int>(
                name: "order_of_execution",
                table: "object_of_labor_technological_procedure",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_of_execution",
                table: "object_of_labor_technological_procedure");

            migrationBuilder.AddColumn<int>(
                name: "order_of_execution",
                table: "technological_procedure",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
