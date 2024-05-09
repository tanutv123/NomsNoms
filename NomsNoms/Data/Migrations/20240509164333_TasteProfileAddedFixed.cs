using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NomsNoms.Data.Migrations
{
    /// <inheritdoc />
    public partial class TasteProfileAddedFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TasteProfile_TasteProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_TasteProfile_TasteProfileId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TasteProfile",
                table: "TasteProfile");

            migrationBuilder.RenameTable(
                name: "TasteProfile",
                newName: "TasteProfiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TasteProfiles",
                table: "TasteProfiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TasteProfiles_TasteProfileId",
                table: "AspNetUsers",
                column: "TasteProfileId",
                principalTable: "TasteProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_TasteProfiles_TasteProfileId",
                table: "Recipes",
                column: "TasteProfileId",
                principalTable: "TasteProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TasteProfiles_TasteProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_TasteProfiles_TasteProfileId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TasteProfiles",
                table: "TasteProfiles");

            migrationBuilder.RenameTable(
                name: "TasteProfiles",
                newName: "TasteProfile");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TasteProfile",
                table: "TasteProfile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TasteProfile_TasteProfileId",
                table: "AspNetUsers",
                column: "TasteProfileId",
                principalTable: "TasteProfile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_TasteProfile_TasteProfileId",
                table: "Recipes",
                column: "TasteProfileId",
                principalTable: "TasteProfile",
                principalColumn: "Id");
        }
    }
}
