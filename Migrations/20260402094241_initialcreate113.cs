using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ManvarFitness.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate113 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomFields_CustomForms_CustomFormId",
                table: "CustomFields");

            migrationBuilder.DropTable(
                name: "FormAnswer");

            migrationBuilder.DropTable(
                name: "FormSubmissions");

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

            migrationBuilder.DropColumn(
                name: "IncludeWeight",
                table: "CustomForms");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CustomForms");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "CustomFields");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "CustomFields");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CustomFields");

            migrationBuilder.DropColumn(
                name: "MaxFileSize",
                table: "CustomFields");

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
                name: "StartTime",
                table: "CustomFields");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CustomForms",
                newName: "CustomFieldData");

            migrationBuilder.RenameColumn(
                name: "IsRequired",
                table: "CustomForms",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "FieldName",
                table: "CustomFields",
                newName: "ValidationData");

            migrationBuilder.RenameColumn(
                name: "IsDefault",
                table: "CustomFields",
                newName: "IsIncluded");

            migrationBuilder.AlterColumn<string>(
                name: "FieldType",
                table: "CustomFields",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomFormId",
                table: "CustomFields",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FieldName",
                table: "CustomFields",
                type: "text",
                nullable: false,
                defaultValue: "");

            

            migrationBuilder.AddForeignKey(
                name: "FK_CustomFields_CustomForms_CustomFormId",
                table: "CustomFields",
                column: "CustomFormId",
                principalTable: "CustomForms",
                principalColumn: "CustomFormId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomFields_CustomForms_CustomFormId",
                table: "CustomFields");


            migrationBuilder.DropColumn(
                name: "FieldName",
                table: "CustomFields");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "CustomForms",
                newName: "IsRequired");

            migrationBuilder.RenameColumn(
                name: "CustomFieldData",
                table: "CustomForms",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ValidationData",
                table: "CustomFields",
                newName: "FieldName");

            migrationBuilder.RenameColumn(
                name: "IsIncluded",
                table: "CustomFields",
                newName: "IsDefault");

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

            migrationBuilder.AddColumn<bool>(
                name: "IncludeWeight",
                table: "CustomForms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CustomForms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "FieldType",
                table: "CustomFields",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "CustomFormId",
                table: "CustomFields",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "CustomFields",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "CustomFields",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CustomFields",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxFileSize",
                table: "CustomFields",
                type: "integer",
                nullable: true);

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

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "CustomFields",
                type: "interval",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FormSubmissions",
                columns: table => new
                {
                    FormSubId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomFormId = table.Column<int>(type: "integer", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: true),
                    Gender = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    HeightFeet = table.Column<int>(type: "integer", nullable: true),
                    HeightInch = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
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
                name: "FormAnswer",
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

            migrationBuilder.AddForeignKey(
                name: "FK_CustomFields_CustomForms_CustomFormId",
                table: "CustomFields",
                column: "CustomFormId",
                principalTable: "CustomForms",
                principalColumn: "CustomFormId");
        }
    }
}
