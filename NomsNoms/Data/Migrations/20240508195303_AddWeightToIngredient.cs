using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NomsNoms.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddWeightToIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StepImageURL",
                table: "RecipeSteps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "Ingredients",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StepImageURL",
                table: "RecipeSteps");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Ingredients");
        }
    }
}
