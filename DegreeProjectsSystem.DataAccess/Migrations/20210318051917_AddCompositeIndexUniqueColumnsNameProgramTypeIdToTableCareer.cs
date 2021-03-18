using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class AddCompositeIndexUniqueColumnsNameProgramTypeIdToTableCareer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Careers_Name_ProgramTypeId",
                table: "Careers",
                columns: new[] { "Name", "ProgramTypeId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Careers_Name_ProgramTypeId",
                table: "Careers");
        }
    }
}
