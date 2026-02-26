using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManvarFitness.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayNumber",
                table: "DietPlans");

            migrationBuilder.AddColumn<string>(
                name: "EmptyStomach",
                table: "DietPlans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Exercise",
                table: "DietPlans",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmptyStomach",
                table: "DietPlans");

            migrationBuilder.DropColumn(
                name: "Exercise",
                table: "DietPlans");

            migrationBuilder.AddColumn<int>(
                name: "DayNumber",
                table: "DietPlans",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
