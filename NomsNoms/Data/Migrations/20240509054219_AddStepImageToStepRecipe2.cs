using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NomsNoms.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStepImageToStepRecipe2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StepImageId",
                table: "RecipeSteps");

            migrationBuilder.AddColumn<int>(
                name: "RecipeStepId",
                table: "RecipeImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeImages_RecipeStepId",
                table: "RecipeImages",
                column: "RecipeStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeImages_RecipeSteps_RecipeStepId",
                table: "RecipeImages",
                column: "RecipeStepId",
                principalTable: "RecipeSteps",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeImages_RecipeSteps_RecipeStepId",
                table: "RecipeImages");

            migrationBuilder.DropIndex(
                name: "IX_RecipeImages_RecipeStepId",
                table: "RecipeImages");

            migrationBuilder.DropColumn(
                name: "RecipeStepId",
                table: "RecipeImages");

            migrationBuilder.AddColumn<int>(
                name: "StepImageId",
                table: "RecipeSteps",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
