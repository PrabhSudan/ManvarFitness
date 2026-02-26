using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ManvarFitness.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomForms",
                table: "CustomForms");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CustomForms");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CustomForms");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CustomForms");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "CustomForms");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "CustomForms",
                newName: "IncludeWeight");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CustomForms",
                newName: "SubConcernId");

            migrationBuilder.AlterColumn<int>(
                name: "SubConcernId",
                table: "CustomForms",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "CustomFormId",
                table: "CustomForms",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeAge",
                table: "CustomForms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeGender",
                table: "CustomForms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeHeight",
                table: "CustomForms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeName",
                table: "CustomForms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomForms",
                table: "CustomForms",
                column: "CustomFormId");

            migrationBuilder.CreateTable(
                name: "CustomFields",
                columns: table => new
                {
                    CustomFieldId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "text", nullable: true),
                    FieldType = table.Column<string>(type: "text", nullable: true),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    CustomFormId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomFields", x => x.CustomFieldId);
                    table.ForeignKey(
                        name: "FK_CustomFields_CustomForms_CustomFormId",
                        column: x => x.CustomFormId,
                        principalTable: "CustomForms",
                        principalColumn: "CustomFormId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormSubmissions",
                columns: table => new
                {
                    FormSubId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
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
                name: "FormAnswers",
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
                name: "IX_CustomForms_SubConcernId",
                table: "CustomForms",
                column: "SubConcernId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomFields_CustomFormId",
                table: "CustomFields",
                column: "CustomFormId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_CustomForms_SubConcerns_SubConcernId",
                table: "CustomForms",
                column: "SubConcernId",
                principalTable: "SubConcerns",
                principalColumn: "SubConcernId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomForms_SubConcerns_SubConcernId",
                table: "CustomForms");

            migrationBuilder.DropTable(
                name: "FormAnswers");

            migrationBuilder.DropTable(
                name: "CustomFields");

            migrationBuilder.DropTable(
                name: "FormSubmissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomForms",
                table: "CustomForms");

            migrationBuilder.DropIndex(
                name: "IX_CustomForms_SubConcernId",
                table: "CustomForms");

            migrationBuilder.DropColumn(
                name: "CustomFormId",
                table: "CustomForms");

            migrationBuilder.DropColumn(
                name: "IncludeAge",
                table: "CustomForms");

            migrationBuilder.DropColumn(
                name: "IncludeGender",
                table: "CustomForms");

            migrationBuilder.DropColumn(
                name: "IncludeHeight",
                table: "CustomForms");

            migrationBuilder.DropColumn(
                name: "IncludeName",
                table: "CustomForms");

            migrationBuilder.RenameColumn(
                name: "SubConcernId",
                table: "CustomForms",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IncludeWeight",
                table: "CustomForms",
                newName: "IsDeleted");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CustomForms",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "CustomForms",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "CustomForms",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "CustomForms",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "CustomForms",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomForms",
                table: "CustomForms",
                column: "Id");
        }
    }
}
