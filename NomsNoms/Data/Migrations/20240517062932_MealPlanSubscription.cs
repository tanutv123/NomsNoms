using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NomsNoms.Data.Migrations
{
    /// <inheritdoc />
    public partial class MealPlanSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MealPlanSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlanSubscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserMealPlanSubscriptions",
                columns: table => new
                {
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    MealPlanSubscriptionId = table.Column<int>(type: "int", nullable: false),
                    StartedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMealPlanSubscriptions", x => new { x.AppUserId, x.MealPlanSubscriptionId });
                    table.ForeignKey(
                        name: "FK_UserMealPlanSubscriptions_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMealPlanSubscriptions_MealPlanSubscriptions_MealPlanSubscriptionId",
                        column: x => x.MealPlanSubscriptionId,
                        principalTable: "MealPlanSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMealPlanSubscriptions_MealPlanSubscriptionId",
                table: "UserMealPlanSubscriptions",
                column: "MealPlanSubscriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMealPlanSubscriptions");

            migrationBuilder.DropTable(
                name: "MealPlanSubscriptions");
        }
    }
}
