using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class fileupload_refactored_with_institutionid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InstitutionId",
                table: "FileUploads",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FileUploads_InstitutionId",
                table: "FileUploads",
                column: "InstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileUploads_Institutions_InstitutionId",
                table: "FileUploads",
                column: "InstitutionId",
                principalTable: "Institutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileUploads_Institutions_InstitutionId",
                table: "FileUploads");

            migrationBuilder.DropIndex(
                name: "IX_FileUploads_InstitutionId",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "FileUploads");
        }
    }
}
