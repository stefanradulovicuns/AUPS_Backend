using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AUPS_Backend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "organizational_unit",
                keyColumn: "organizational_unit_id",
                keyValue: new Guid("2fde0af9-31a5-479b-ae3f-f5c7c85ee086"));

            migrationBuilder.InsertData(
                table: "organizational_unit",
                columns: new[] { "organizational_unit_id", "organizational_unit_name" },
                values: new object[] { new Guid("cb34a64d-9adc-49ba-af1c-86cfe760659c"), "Administracija" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "organizational_unit",
                keyColumn: "organizational_unit_id",
                keyValue: new Guid("cb34a64d-9adc-49ba-af1c-86cfe760659c"));

            migrationBuilder.InsertData(
                table: "organizational_unit",
                columns: new[] { "organizational_unit_id", "organizational_unit_name" },
                values: new object[] { new Guid("2fde0af9-31a5-479b-ae3f-f5c7c85ee086"), "Administracija" });
        }
    }
}
