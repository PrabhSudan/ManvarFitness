using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManvarFitness.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomForms_SubConcerns_SubConcernId",
                table: "CustomForms");

            migrationBuilder.AlterColumn<int>(
                name: "SubConcernId",
                table: "CustomForms",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomForms_SubConcerns_SubConcernId",
                table: "CustomForms",
                column: "SubConcernId",
                principalTable: "SubConcerns",
                principalColumn: "SubConcernId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomForms_SubConcerns_SubConcernId",
                table: "CustomForms");

            migrationBuilder.AlterColumn<int>(
                name: "SubConcernId",
                table: "CustomForms",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomForms_SubConcerns_SubConcernId",
                table: "CustomForms",
                column: "SubConcernId",
                principalTable: "SubConcerns",
                principalColumn: "SubConcernId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
