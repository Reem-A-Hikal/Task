using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeCustomProp.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PropertyDefinitions",
                columns: new[] { "Id", "DropdownOptions", "IsRequired", "Name", "Type" },
                values: new object[,]
                {
                    { 1, null, true, "Age", "Integer" },
                    { 2, "HR,IT,Finance", false, "Department", "Dropdown" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PropertyDefinitions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PropertyDefinitions",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
