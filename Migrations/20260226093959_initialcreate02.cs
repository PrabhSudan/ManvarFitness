using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ManvarFitness.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxLength",
                table: "CustomFields",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxValue",
                table: "CustomFields",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinValue",
                table: "CustomFields",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Options",
                table: "CustomFields",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FormSubmissions",
                columns: table => new
                {
                    FormSubId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: true),
                    Gender = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    HeightFeet = table.Column<int>(type: "integer", nullable: true),
                    HeightInch = table.Column<int>(type: "integer", nullable: true),
                    Weight = table.Column<decimal>(type: "numeric", nullable: true),
                    CustomFormId = table.Column<int>(type: "integer", nullable: false)
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
                name: "FormAnswer",
                columns: table => new
                {
                    FormAnsId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomFieldId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    FormSubId = table.Column<int>(type: "integer", nullable: false),
                    FormSubmissionFormSubId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormAnswer", x => x.FormAnsId);
                    table.ForeignKey(
                        name: "FK_FormAnswer_CustomFields_CustomFieldId",
                        column: x => x.CustomFieldId,
                        principalTable: "CustomFields",
                        principalColumn: "CustomFieldId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormAnswer_FormSubmissions_FormSubmissionFormSubId",
                        column: x => x.FormSubmissionFormSubId,
                        principalTable: "FormSubmissions",
                        principalColumn: "FormSubId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormAnswer_CustomFieldId",
                table: "FormAnswer",
                column: "CustomFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_FormAnswer_FormSubmissionFormSubId",
                table: "FormAnswer",
                column: "FormSubmissionFormSubId");

            migrationBuilder.CreateIndex(
                name: "IX_FormSubmissions_CustomFormId",
                table: "FormSubmissions",
                column: "CustomFormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormAnswer");

            migrationBuilder.DropTable(
                name: "FormSubmissions");

            migrationBuilder.DropColumn(
                name: "MaxLength",
                table: "CustomFields");

            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "CustomFields");

            migrationBuilder.DropColumn(
                name: "MinValue",
                table: "CustomFields");

            migrationBuilder.DropColumn(
                name: "Options",
                table: "CustomFields");
        }
    }
}
