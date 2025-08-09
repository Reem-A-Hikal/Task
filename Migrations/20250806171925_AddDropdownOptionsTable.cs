using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeCustomProp.Migrations
{
    /// <inheritdoc />
    public partial class AddDropdownOptionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DropdownOptions",
                table: "PropertyDefinitions");

            migrationBuilder.CreateTable(
                name: "DropdownOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyDefinitionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DropdownOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DropdownOptions_PropertyDefinitions_PropertyDefinitionId",
                        column: x => x.PropertyDefinitionId,
                        principalTable: "PropertyDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DropdownOptions",
                columns: new[] { "Id", "PropertyDefinitionId", "Value" },
                values: new object[,]
                {
                    { 1, 2, "HR" },
                    { 2, 2, "IT" },
                    { 3, 2, "Finance" }
                });

            migrationBuilder.UpdateData(
                table: "PropertyDefinitions",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsRequired",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_DropdownOptions_PropertyDefinitionId",
                table: "DropdownOptions",
                column: "PropertyDefinitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DropdownOptions");

            migrationBuilder.AddColumn<string>(
                name: "DropdownOptions",
                table: "PropertyDefinitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "PropertyDefinitions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DropdownOptions",
                value: null);

            migrationBuilder.UpdateData(
                table: "PropertyDefinitions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DropdownOptions", "IsRequired" },
                values: new object[] { "HR,IT,Finance", false });
        }
    }
}
