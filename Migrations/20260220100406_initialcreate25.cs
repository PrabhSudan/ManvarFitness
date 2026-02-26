using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManvarFitness.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomFields_CustomForms_CustomFormId",
                table: "CustomFields");

            migrationBuilder.CreateIndex(
                name: "IX_CustomFields_CustomFormId",
                table: "CustomFields",
                column: "CustomFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomFields_CustomForms_CustomFormId",
                table: "CustomFields",
                column: "CustomFormId",
                principalTable: "CustomForms",
                principalColumn: "CustomFormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomFields_CustomForms_CustomFormId",
                table: "CustomFields");

            migrationBuilder.DropIndex(
                name: "IX_CustomFields_CustomFormId",
                table: "CustomFields");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomFields_CustomForms_CustomFormId",
                table: "CustomFields",
                column: "CustomFormId",
                principalTable: "CustomForms",
                principalColumn: "CustomFormId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
