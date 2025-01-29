using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShortLink.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateNewPropertyInShortUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OriginalUrlId",
                table: "ShortUrls",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ShortUrls_OriginalUrlId",
                table: "ShortUrls",
                column: "OriginalUrlId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShortUrls_OriginalUrls_OriginalUrlId",
                table: "ShortUrls",
                column: "OriginalUrlId",
                principalTable: "OriginalUrls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShortUrls_OriginalUrls_OriginalUrlId",
                table: "ShortUrls");

            migrationBuilder.DropIndex(
                name: "IX_ShortUrls_OriginalUrlId",
                table: "ShortUrls");

            migrationBuilder.DropColumn(
                name: "OriginalUrlId",
                table: "ShortUrls");
        }
    }
}
