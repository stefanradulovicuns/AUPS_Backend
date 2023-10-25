using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AUPS_Backend.Migrations
{
    /// <inheritdoc />
    public partial class order_of_execution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "order_of_execution",
                table: "technological_procedure",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "current_technological_procedure",
                table: "production_order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_of_execution",
                table: "technological_procedure");

            migrationBuilder.DropColumn(
                name: "current_technological_procedure",
                table: "production_order");
        }
    }
}
