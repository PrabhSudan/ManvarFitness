using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManvarFitness.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rolepages",
                table: "rolepages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pages",
                table: "pages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_adminusers",
                table: "adminusers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_workout",
                table: "workout");

            migrationBuilder.RenameTable(
                name: "roles",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "rolepages",
                newName: "RolePages");

            migrationBuilder.RenameTable(
                name: "pages",
                newName: "Pages");

            migrationBuilder.RenameTable(
                name: "adminusers",
                newName: "AdminUsers");

            migrationBuilder.RenameTable(
                name: "workout",
                newName: "WorkoutVideos");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminUsers",
                table: "AdminUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutVideos",
                table: "WorkoutVideos",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePages",
                table: "RolePages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pages",
                table: "Pages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminUsers",
                table: "AdminUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutVideos",
                table: "WorkoutVideos");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "roles");

            migrationBuilder.RenameTable(
                name: "RolePages",
                newName: "rolepages");

            migrationBuilder.RenameTable(
                name: "Pages",
                newName: "pages");

            migrationBuilder.RenameTable(
                name: "AdminUsers",
                newName: "adminusers");

            migrationBuilder.RenameTable(
                name: "WorkoutVideos",
                newName: "workout");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                table: "roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rolepages",
                table: "rolepages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pages",
                table: "pages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_adminusers",
                table: "adminusers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_workout",
                table: "workout",
                column: "Id");
        }
    }
}
