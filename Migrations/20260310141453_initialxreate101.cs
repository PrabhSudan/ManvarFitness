using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManvarFitness.Migrations
{
    /// <inheritdoc />
    public partial class initialxreate101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxDate",
                table: "CustomFields");

            migrationBuilder.RenameColumn(
                name: "MinDate",
                table: "CustomFields",
                newName: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "CustomFields",
                newName: "MinDate");

            migrationBuilder.AddColumn<DateOnly>(
                name: "MaxDate",
                table: "CustomFields",
                type: "date",
                nullable: true);
        }
    }
}
