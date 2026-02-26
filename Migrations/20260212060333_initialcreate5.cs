using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManvarFitness.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_rolePages",
                table: "rolePages");

            migrationBuilder.RenameTable(
                name: "rolePages",
                newName: "rolepages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rolepages",
                table: "rolepages",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_rolepages",
                table: "rolepages");

            migrationBuilder.RenameTable(
                name: "rolepages",
                newName: "rolePages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rolePages",
                table: "rolePages",
                column: "Id");
        }
    }
}
