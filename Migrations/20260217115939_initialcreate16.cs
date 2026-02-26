using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ManvarFitness.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "DietPlans");

            migrationBuilder.CreateTable(
                name: "SubConcerns",
                columns: table => new
                {
                    SubConcernId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false),
                    ConcernCategoryid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubConcerns", x => x.SubConcernId);
                    table.ForeignKey(
                        name: "FK_SubConcerns_Categories_ConcernCategoryid",
                        column: x => x.ConcernCategoryid,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });


            migrationBuilder.CreateIndex(
                name: "IX_SubConcerns_ConcernCategoryid",
                table: "SubConcerns",
                column: "ConcernCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubConcerns");

           
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "DietPlans",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
