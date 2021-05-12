using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class CorrectionAttributeEducationLevelIdInEntityRecognition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recognitions_EducationLevels_EducationLevelId",
                table: "Recognitions");

            migrationBuilder.DropColumn(
                name: "ProgramTypeId",
                table: "Recognitions");

            migrationBuilder.AlterColumn<int>(
                name: "EducationLevelId",
                table: "Recognitions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recognitions_EducationLevels_EducationLevelId",
                table: "Recognitions",
                column: "EducationLevelId",
                principalTable: "EducationLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recognitions_EducationLevels_EducationLevelId",
                table: "Recognitions");

            migrationBuilder.AlterColumn<int>(
                name: "EducationLevelId",
                table: "Recognitions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProgramTypeId",
                table: "Recognitions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Recognitions_EducationLevels_EducationLevelId",
                table: "Recognitions",
                column: "EducationLevelId",
                principalTable: "EducationLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
