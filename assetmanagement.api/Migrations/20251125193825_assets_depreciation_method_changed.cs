using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class assets_depreciation_method_changed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DepreciationMethod",
                table: "Assets",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DepreciationMethod",
                table: "Assets",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
