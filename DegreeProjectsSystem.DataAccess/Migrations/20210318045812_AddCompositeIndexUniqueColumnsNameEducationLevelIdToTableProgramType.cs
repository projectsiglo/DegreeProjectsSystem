using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class AddCompositeIndexUniqueColumnsNameEducationLevelIdToTableProgramType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProgramTypes_Name_EducationLevelId",
                table: "ProgramTypes",
                columns: new[] { "Name", "EducationLevelId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProgramTypes_Name_EducationLevelId",
                table: "ProgramTypes");
        }
    }
}
