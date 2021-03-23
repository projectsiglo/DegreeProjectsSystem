using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class ChangeNameAtributtesTelefonoByPhoneAndCorreoByEmailTableInstitution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Correo",
                table: "Institutions");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Institutions");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Institutions",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Institutions",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Institutions");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Institutions");

            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "Institutions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Institutions",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }
    }
}
