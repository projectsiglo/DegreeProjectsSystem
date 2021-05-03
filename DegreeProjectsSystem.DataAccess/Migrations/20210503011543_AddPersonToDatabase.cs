using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class AddPersonToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentificationNumber = table.Column<string>(nullable: false),
                    IdentityDocumentTypeId = table.Column<int>(nullable: false),
                    Names = table.Column<string>(maxLength: 100, nullable: false),
                    Surnames = table.Column<string>(maxLength: 100, nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(maxLength: 300, nullable: false),
                    Phone = table.Column<string>(maxLength: 10, nullable: false),
                    Mobile = table.Column<string>(maxLength: 10, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_People_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_People_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_People_IdentityDocumentTypes_IdentityDocumentTypeId",
                        column: x => x.IdentityDocumentTypeId,
                        principalTable: "IdentityDocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_CityId",
                table: "People",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_People_DepartmentId",
                table: "People",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_People_GenderId",
                table: "People",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_People_IdentificationNumber",
                table: "People",
                column: "IdentificationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_IdentityDocumentTypeId",
                table: "People",
                column: "IdentityDocumentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
