﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class AddIndexUniqueToTableDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name",
                table: "Departments",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Departments_Name",
                table: "Departments");
        }
    }
}
