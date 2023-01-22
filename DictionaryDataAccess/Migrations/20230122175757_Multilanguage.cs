using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DictionaryDataAccess.Migrations
{
    public partial class Multilanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "English",
                table: "Localizations");

            migrationBuilder.DropColumn(
                name: "Hungarian",
                table: "Localizations");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Localizations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phrase",
                table: "Localizations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RowId",
                table: "Localizations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Localizations_Language",
                table: "Localizations",
                column: "Language");

            migrationBuilder.CreateIndex(
                name: "IX_Localizations_Phrase",
                table: "Localizations",
                column: "Phrase");

            migrationBuilder.CreateIndex(
                name: "IX_Localizations_RowId",
                table: "Localizations",
                column: "RowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Localizations_Language",
                table: "Localizations");

            migrationBuilder.DropIndex(
                name: "IX_Localizations_Phrase",
                table: "Localizations");

            migrationBuilder.DropIndex(
                name: "IX_Localizations_RowId",
                table: "Localizations");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Localizations");

            migrationBuilder.DropColumn(
                name: "Phrase",
                table: "Localizations");

            migrationBuilder.DropColumn(
                name: "RowId",
                table: "Localizations");

            migrationBuilder.AddColumn<string>(
                name: "English",
                table: "Localizations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Hungarian",
                table: "Localizations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
