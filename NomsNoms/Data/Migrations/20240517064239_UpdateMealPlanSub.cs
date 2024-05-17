using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NomsNoms.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMealPlanSub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MealPlanSubscriptions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "MealPlanSubscriptions");
        }
    }
}
