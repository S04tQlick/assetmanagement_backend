using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class fileupload_islogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Institutions_InstitutionsModelId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_InstitutionsModelId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "InstitutionsModelId",
                table: "Addresses");

            migrationBuilder.AddColumn<bool>(
                name: "IsLogo",
                table: "FileUploads",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLogo",
                table: "FileUploads");

            migrationBuilder.AddColumn<Guid>(
                name: "InstitutionsModelId",
                table: "Addresses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_InstitutionsModelId",
                table: "Addresses",
                column: "InstitutionsModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Institutions_InstitutionsModelId",
                table: "Addresses",
                column: "InstitutionsModelId",
                principalTable: "Institutions",
                principalColumn: "Id");
        }
    }
}
