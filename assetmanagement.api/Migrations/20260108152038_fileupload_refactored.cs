using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class fileupload_refactored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileUploadsModel",
                table: "FileUploadsModel");

            migrationBuilder.RenameTable(
                name: "FileUploadsModel",
                newName: "FileUploads");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileUploads",
                table: "FileUploads",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileUploads",
                table: "FileUploads");

            migrationBuilder.RenameTable(
                name: "FileUploads",
                newName: "FileUploadsModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileUploadsModel",
                table: "FileUploadsModel",
                column: "Id");
        }
    }
}
