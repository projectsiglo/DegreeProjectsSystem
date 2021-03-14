using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class AddCompositeIndexUniqueColumnsNameDepartamentIdToTableCities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name_DepartmentId",
                table: "Cities",
                columns: new[] { "Name", "DepartmentId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cities_Name_DepartmentId",
                table: "Cities");
        }
    }
}
