using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManvarFitness.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate012 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcernId",
                table: "UserDietPlans");

            migrationBuilder.AddColumn<long>(
                name: "UserConcernId",
                table: "UserDietPlans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserConcernId",
                table: "UserDietPlans");

            migrationBuilder.AddColumn<int>(
                name: "ConcernId",
                table: "UserDietPlans",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
