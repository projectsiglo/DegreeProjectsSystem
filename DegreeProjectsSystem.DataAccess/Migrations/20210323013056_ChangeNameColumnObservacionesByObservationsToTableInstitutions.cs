using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class ChangeNameColumnObservacionesByObservationsToTableInstitutions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "Institutions");

            migrationBuilder.AddColumn<string>(
                name: "Observations",
                table: "Institutions",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observations",
                table: "Institutions");

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "Institutions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
