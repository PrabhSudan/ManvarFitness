using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManvarFitness.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConcernId",
                table: "CustomForms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomForms_ConcernId",
                table: "CustomForms",
                column: "ConcernId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomForms_Concerns_ConcernId",
                table: "CustomForms",
                column: "ConcernId",
                principalTable: "Concerns",
                principalColumn: "ConcernId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomForms_Concerns_ConcernId",
                table: "CustomForms");

            migrationBuilder.DropIndex(
                name: "IX_CustomForms_ConcernId",
                table: "CustomForms");

            migrationBuilder.DropColumn(
                name: "ConcernId",
                table: "CustomForms");
        }
    }
}
