using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class assets_depreciation_method : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepreciationMethodEnum",
                table: "Assets",
                newName: "DepreciationMethod");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepreciationMethod",
                table: "Assets",
                newName: "DepreciationMethodEnum");
        }
    }
}
