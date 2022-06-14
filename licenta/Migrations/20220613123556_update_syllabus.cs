using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace licenta.Migrations
{
    public partial class update_syllabus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections2_Subjects_SubjectId",
                table: "Sections2");

            migrationBuilder.DropIndex(
                name: "IX_Sections2_SubjectId",
                table: "Sections2");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Sections2");

            migrationBuilder.AddColumn<Guid>(
                name: "Section1Id",
                table: "Syllabuses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Section2Id",
                table: "Syllabuses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SyllabusTeachers",
                columns: table => new
                {
                    SyllabusId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyllabusTeachers", x => new { x.TeacherId, x.SyllabusId });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Syllabuses_Section1Id",
                table: "Syllabuses",
                column: "Section1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Syllabuses_Section2Id",
                table: "Syllabuses",
                column: "Section2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Syllabuses_Sections1_Section1Id",
                table: "Syllabuses",
                column: "Section1Id",
                principalTable: "Sections1",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Syllabuses_Sections2_Section2Id",
                table: "Syllabuses",
                column: "Section2Id",
                principalTable: "Sections2",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Sections1_Section1Id",
                table: "Syllabuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Sections2_Section2Id",
                table: "Syllabuses");

            migrationBuilder.DropTable(
                name: "SyllabusTeachers");

            migrationBuilder.DropIndex(
                name: "IX_Syllabuses_Section1Id",
                table: "Syllabuses");

            migrationBuilder.DropIndex(
                name: "IX_Syllabuses_Section2Id",
                table: "Syllabuses");

            migrationBuilder.DropColumn(
                name: "Section1Id",
                table: "Syllabuses");

            migrationBuilder.DropColumn(
                name: "Section2Id",
                table: "Syllabuses");

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "Sections2",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Sections2_SubjectId",
                table: "Sections2",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections2_Subjects_SubjectId",
                table: "Sections2",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
