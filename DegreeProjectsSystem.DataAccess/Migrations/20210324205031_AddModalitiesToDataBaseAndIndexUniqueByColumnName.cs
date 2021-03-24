using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class AddModalitiesToDataBaseAndIndexUniqueByColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modalities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    EducationLevelId = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modalities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modalities_EducationLevels_EducationLevelId",
                        column: x => x.EducationLevelId,
                        principalTable: "EducationLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modalities_EducationLevelId",
                table: "Modalities",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Modalities_Name",
                table: "Modalities",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Modalities");
        }
    }
}
