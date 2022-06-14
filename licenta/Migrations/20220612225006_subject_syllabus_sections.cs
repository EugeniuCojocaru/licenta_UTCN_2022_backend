using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace licenta.Migrations
{
    public partial class subject_syllabus_sections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Syllabuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Syllabuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Syllabuses_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections1",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionId = table.Column<Guid>(type: "uuid", nullable: false),
                    FacultyId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    FieldOfStudyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CycleOfStudy = table.Column<string>(type: "text", nullable: false),
                    ProgramOfStudy = table.Column<string>(type: "text", nullable: false),
                    Qualification = table.Column<string>(type: "text", nullable: false),
                    FormOfEducation = table.Column<string>(type: "text", nullable: false),
                    SyllabusId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections1_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sections1_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sections1_FieldsOfStudy_FieldOfStudyId",
                        column: x => x.FieldOfStudyId,
                        principalTable: "FieldsOfStudy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sections1_Institutions_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sections1_Syllabuses_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "Syllabuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections2",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    YearOfStudy = table.Column<int>(type: "integer", nullable: false),
                    Semester = table.Column<int>(type: "integer", nullable: false),
                    Assessment = table.Column<int>(type: "integer", nullable: false),
                    Category1 = table.Column<int>(type: "integer", nullable: false),
                    Category2 = table.Column<int>(type: "integer", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false),
                    SyllabusId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections2_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sections2_Syllabuses_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "Syllabuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sections2_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections3",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseHoursPerWeek = table.Column<int>(type: "integer", nullable: false),
                    SeminarHoursPerWeek = table.Column<int>(type: "integer", nullable: false),
                    LaboratoryHoursPerWeek = table.Column<int>(type: "integer", nullable: false),
                    ProjectHoursPerWeek = table.Column<int>(type: "integer", nullable: false),
                    CourseHoursPerSemester = table.Column<int>(type: "integer", nullable: false),
                    SeminarHoursPerSemester = table.Column<int>(type: "integer", nullable: false),
                    LaboratoryHoursPerSemester = table.Column<int>(type: "integer", nullable: false),
                    ProjectHoursPerSemester = table.Column<int>(type: "integer", nullable: false),
                    IndividualStudyA = table.Column<int>(type: "integer", nullable: false),
                    IndividualStudyB = table.Column<int>(type: "integer", nullable: false),
                    IndividualStudyC = table.Column<int>(type: "integer", nullable: false),
                    IndividualStudyD = table.Column<int>(type: "integer", nullable: false),
                    IndividualStudyE = table.Column<int>(type: "integer", nullable: false),
                    IndividualStudyF = table.Column<int>(type: "integer", nullable: false),
                    Credits = table.Column<int>(type: "integer", nullable: false),
                    SyllabusId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections3", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections3_Syllabuses_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "Syllabuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sections1_DepartmentId",
                table: "Sections1",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections1_FacultyId",
                table: "Sections1",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections1_FieldOfStudyId",
                table: "Sections1",
                column: "FieldOfStudyId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections1_InstitutionId",
                table: "Sections1",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections1_SyllabusId",
                table: "Sections1",
                column: "SyllabusId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections2_SubjectId",
                table: "Sections2",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections2_SyllabusId",
                table: "Sections2",
                column: "SyllabusId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections2_TeacherId",
                table: "Sections2",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections3_SyllabusId",
                table: "Sections3",
                column: "SyllabusId");

            migrationBuilder.CreateIndex(
                name: "IX_Syllabuses_SubjectId",
                table: "Syllabuses",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sections1");

            migrationBuilder.DropTable(
                name: "Sections2");

            migrationBuilder.DropTable(
                name: "Sections3");

            migrationBuilder.DropTable(
                name: "Syllabuses");
        }
    }
}
