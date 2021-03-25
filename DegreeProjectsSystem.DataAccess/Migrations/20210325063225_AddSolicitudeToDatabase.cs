using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class AddSolicitudeToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TitleDegreework",
                table: "Solicitudes",
                newName: "TitleDegreeWork");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TitleDegreeWork",
                table: "Solicitudes",
                newName: "TitleDegreework");
        }
    }
}
