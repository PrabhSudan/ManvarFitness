using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManvarFitness.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Workout",
                table: "Workout");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePages",
                table: "RolePages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pages",
                table: "Pages");

            migrationBuilder.RenameTable(
                name: "Workout",
                newName: "workout");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "roles");

            migrationBuilder.RenameTable(
                name: "RolePages",
                newName: "rolePages");

            migrationBuilder.RenameTable(
                name: "Pages",
                newName: "pages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_workout",
                table: "workout",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                table: "roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rolePages",
                table: "rolePages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pages",
                table: "pages",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_workout",
                table: "workout");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rolePages",
                table: "rolePages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pages",
                table: "pages");

            migrationBuilder.RenameTable(
                name: "workout",
                newName: "Workout");

            migrationBuilder.RenameTable(
                name: "roles",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "rolePages",
                newName: "RolePages");

            migrationBuilder.RenameTable(
                name: "pages",
                newName: "Pages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workout",
                table: "Workout",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePages",
                table: "RolePages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pages",
                table: "Pages",
                column: "Id");
        }
    }
}
