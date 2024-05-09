using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NomsNoms.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStepImageToStepRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StepImageURL",
                table: "RecipeSteps");

            migrationBuilder.AddColumn<int>(
                name: "StepImageId",
                table: "RecipeSteps",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StepImageId",
                table: "RecipeSteps");

            migrationBuilder.AddColumn<string>(
                name: "StepImageURL",
                table: "RecipeSteps",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
