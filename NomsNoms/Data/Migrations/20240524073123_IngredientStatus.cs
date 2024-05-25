using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NomsNoms.Data.Migrations
{
    /// <inheritdoc />
    public partial class IngredientStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Ingredients",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Ingredients");
        }
    }
}
