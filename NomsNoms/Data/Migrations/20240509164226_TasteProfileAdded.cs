using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NomsNoms.Data.Migrations
{
    /// <inheritdoc />
    public partial class TasteProfileAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserPhotos_AppUserId",
                table: "UserPhotos");

            migrationBuilder.AddColumn<int>(
                name: "TasteProfileId",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TasteProfileId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TasteProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Spiciness = table.Column<int>(type: "int", nullable: false),
                    Sweetness = table.Column<int>(type: "int", nullable: false),
                    Saltiness = table.Column<int>(type: "int", nullable: false),
                    Sauce = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasteProfile", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPhotos_AppUserId",
                table: "UserPhotos",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_TasteProfileId",
                table: "Recipes",
                column: "TasteProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TasteProfileId",
                table: "AspNetUsers",
                column: "TasteProfileId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TasteProfile_TasteProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_TasteProfile_TasteProfileId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "TasteProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserPhotos_AppUserId",
                table: "UserPhotos");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_TasteProfileId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TasteProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TasteProfileId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "TasteProfileId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhotos_AppUserId",
                table: "UserPhotos",
                column: "AppUserId");
        }
    }
}
