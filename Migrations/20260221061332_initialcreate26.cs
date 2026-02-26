using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ManvarFitness.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormAnswers");

            migrationBuilder.DropTable(
                name: "FormSubmissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormSubmissions",
                columns: table => new
                {
                    FormSubId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomFormId = table.Column<int>(type: "integer", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    HeightFeet = table.Column<int>(type: "integer", nullable: true),
                    HeightInch = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormSubmissions", x => x.FormSubId);
                    table.ForeignKey(
                        name: "FK_FormSubmissions_CustomForms_CustomFormId",
                        column: x => x.CustomFormId,
                        principalTable: "CustomForms",
                        principalColumn: "CustomFormId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormAnswers",
                columns: table => new
                {
                    FormAnsId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomFieldId = table.Column<int>(type: "integer", nullable: false),
                    FormSubmissionFormSubId = table.Column<int>(type: "integer", nullable: true),
                    FormSubId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormAnswers", x => x.FormAnsId);
                    table.ForeignKey(
                        name: "FK_FormAnswers_CustomFields_CustomFieldId",
                        column: x => x.CustomFieldId,
                        principalTable: "CustomFields",
                        principalColumn: "CustomFieldId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormAnswers_FormSubmissions_FormSubmissionFormSubId",
                        column: x => x.FormSubmissionFormSubId,
                        principalTable: "FormSubmissions",
                        principalColumn: "FormSubId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormAnswers_CustomFieldId",
                table: "FormAnswers",
                column: "CustomFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FormAnswers_FormSubmissionFormSubId",
                table: "FormAnswers",
                column: "FormSubmissionFormSubId");

            migrationBuilder.CreateIndex(
                name: "IX_FormSubmissions_CustomFormId",
                table: "FormSubmissions",
                column: "CustomFormId");
        }
    }
}
