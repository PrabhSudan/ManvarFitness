using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManvarFitness.Migrations
{
    /// <inheritdoc />
    public partial class intialcreate001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "CustomFields",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MaxDate",
                table: "CustomFields",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxFileSize",
                table: "CustomFields",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MinDate",
                table: "CustomFields",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "CustomFields",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoType",
                table: "CustomFields",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "CustomFields");

            migrationBuilder.DropColumn(
                name: "MaxDate",
                table: "CustomFields");

            migrationBuilder.DropColumn(
                name: "MaxFileSize",
                table: "CustomFields");

            migrationBuilder.DropColumn(
                name: "MinDate",
                table: "CustomFields");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "CustomFields");

            migrationBuilder.DropColumn(
                name: "VideoType",
                table: "CustomFields");
        }
    }
}
