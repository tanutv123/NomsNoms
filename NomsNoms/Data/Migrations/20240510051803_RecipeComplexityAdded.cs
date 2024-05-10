using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NomsNoms.Data.Migrations
{
    /// <inheritdoc />
    public partial class RecipeComplexityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipeComplexityId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RecipeComplexities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeComplexities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_RecipeComplexityId",
                table: "Recipes",
                column: "RecipeComplexityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_RecipeComplexities_RecipeComplexityId",
                table: "Recipes",
                column: "RecipeComplexityId",
                principalTable: "RecipeComplexities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_RecipeComplexities_RecipeComplexityId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "RecipeComplexities");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_RecipeComplexityId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RecipeComplexityId",
                table: "Recipes");
        }
    }
}
