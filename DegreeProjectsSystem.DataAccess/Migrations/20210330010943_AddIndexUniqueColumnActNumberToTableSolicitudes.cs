using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class AddIndexUniqueColumnActNumberToTableSolicitudes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_ActNumber",
                table: "Solicitudes",
                column: "ActNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Solicitudes_ActNumber",
                table: "Solicitudes");
        }
    }
}
