using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace licenta.Migrations
{
    public partial class section10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Section10Id",
                table: "Syllabuses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sections10",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseCriteria = table.Column<string>(type: "text", nullable: false),
                    CourseMethods = table.Column<string>(type: "text", nullable: false),
                    CourcePercentage = table.Column<int>(type: "integer", nullable: false),
                    SeminarCriteria = table.Column<string>(type: "text", nullable: false),
                    SeminarMethods = table.Column<string>(type: "text", nullable: false),
                    SeminarPercentage = table.Column<int>(type: "integer", nullable: false),
                    LaboratoryCriteria = table.Column<string>(type: "text", nullable: false),
                    LaboratoryMethods = table.Column<string>(type: "text", nullable: false),
                    LaboratoryPercentage = table.Column<int>(type: "integer", nullable: false),
                    ProjectCriteria = table.Column<string>(type: "text", nullable: false),
                    ProjectMethods = table.Column<string>(type: "text", nullable: false),
                    ProjectPercentage = table.Column<int>(type: "integer", nullable: false),
                    MinimumPerformance = table.Column<string>(type: "text", nullable: false),
                    ConditionsFinalExam = table.Column<string>(type: "text", nullable: false),
                    ConditionsPromotion = table.Column<string>(type: "text", nullable: false),
                    SyllabusId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections10", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections10_Syllabuses_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "Syllabuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Syllabuses_Section10Id",
                table: "Syllabuses",
                column: "Section10Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sections10_SyllabusId",
                table: "Sections10",
                column: "SyllabusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Syllabuses_Sections10_Section10Id",
                table: "Syllabuses",
                column: "Section10Id",
                principalTable: "Sections10",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Sections10_Section10Id",
                table: "Syllabuses");

            migrationBuilder.DropTable(
                name: "Sections10");

            migrationBuilder.DropIndex(
                name: "IX_Syllabuses_Section10Id",
                table: "Syllabuses");

            migrationBuilder.DropColumn(
                name: "Section10Id",
                table: "Syllabuses");
        }
    }
}
