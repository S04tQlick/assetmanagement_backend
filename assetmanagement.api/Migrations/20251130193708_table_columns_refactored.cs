using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class table_columns_refactored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Institutions",
                newName: "InstitutionName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Institutions",
                newName: "InstitutionEmail");

            migrationBuilder.RenameColumn(
                name: "ContactNumber",
                table: "Institutions",
                newName: "InstitutionContactNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Branches",
                newName: "BranchName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Assets",
                newName: "AssetName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InstitutionName",
                table: "Institutions",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "InstitutionEmail",
                table: "Institutions",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "InstitutionContactNumber",
                table: "Institutions",
                newName: "ContactNumber");

            migrationBuilder.RenameColumn(
                name: "BranchName",
                table: "Branches",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "AssetName",
                table: "Assets",
                newName: "Name");
        }
    }
}
