using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace licenta.Migrations
{
    public partial class add_sections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Section3Id",
                table: "Syllabuses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Section4Id",
                table: "Syllabuses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Section5Id",
                table: "Syllabuses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Section6Id",
                table: "Syllabuses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Section7Id",
                table: "Syllabuses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Section8Id",
                table: "Syllabuses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Section9Id",
                table: "Syllabuses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Section4Subjects",
                columns: table => new
                {
                    Section4Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section4Subjects", x => new { x.SubjectId, x.Section4Id });
                });

            migrationBuilder.CreateTable(
                name: "Sections4",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Competences = table.Column<string>(type: "text", nullable: false),
                    SyllabusId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections4", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections4_Syllabuses_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "Syllabuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections5",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Course = table.Column<string>(type: "text", nullable: false),
                    Application = table.Column<string>(type: "text", nullable: false),
                    SyllabusId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections5", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections5_Syllabuses_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "Syllabuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections6",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Professional = table.Column<string>(type: "text", nullable: false),
                    Cross = table.Column<string>(type: "text", nullable: false),
                    SyllabusId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections6", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections6_Syllabuses_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "Syllabuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections7",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GeneralObjective = table.Column<string>(type: "text", nullable: false),
                    SpecificObjectives = table.Column<string>(type: "text", nullable: false),
                    SyllabusId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections7", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections7_Syllabuses_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "Syllabuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections8",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeachingMethodsCourse = table.Column<string>(type: "text", nullable: false),
                    TeachingMethodsLab = table.Column<string>(type: "text", nullable: false),
                    BibliographyCourse = table.Column<string>(type: "text", nullable: false),
                    BibliographyLab = table.Column<string>(type: "text", nullable: false),
                    SyllabusId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections8", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections8_Syllabuses_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "Syllabuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections9",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    SyllabusId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections9", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections9_Syllabuses_SyllabusId",
                        column: x => x.SyllabusId,
                        principalTable: "Syllabuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Section8Elements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsCourse = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    Section8Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section8Elements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Section8Elements_Sections8_Section8Id",
                        column: x => x.Section8Id,
                        principalTable: "Sections8",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Syllabuses_Section3Id",
                table: "Syllabuses",
                column: "Section3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Syllabuses_Section4Id",
                table: "Syllabuses",
                column: "Section4Id");

            migrationBuilder.CreateIndex(
                name: "IX_Syllabuses_Section5Id",
                table: "Syllabuses",
                column: "Section5Id");

            migrationBuilder.CreateIndex(
                name: "IX_Syllabuses_Section6Id",
                table: "Syllabuses",
                column: "Section6Id");

            migrationBuilder.CreateIndex(
                name: "IX_Syllabuses_Section7Id",
                table: "Syllabuses",
                column: "Section7Id");

            migrationBuilder.CreateIndex(
                name: "IX_Syllabuses_Section8Id",
                table: "Syllabuses",
                column: "Section8Id");

            migrationBuilder.CreateIndex(
                name: "IX_Syllabuses_Section9Id",
                table: "Syllabuses",
                column: "Section9Id");

            migrationBuilder.CreateIndex(
                name: "IX_Section8Elements_Section8Id",
                table: "Section8Elements",
                column: "Section8Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sections4_SyllabusId",
                table: "Sections4",
                column: "SyllabusId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections5_SyllabusId",
                table: "Sections5",
                column: "SyllabusId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections6_SyllabusId",
                table: "Sections6",
                column: "SyllabusId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections7_SyllabusId",
                table: "Sections7",
                column: "SyllabusId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections8_SyllabusId",
                table: "Sections8",
                column: "SyllabusId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections9_SyllabusId",
                table: "Sections9",
                column: "SyllabusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Syllabuses_Sections3_Section3Id",
                table: "Syllabuses",
                column: "Section3Id",
                principalTable: "Sections3",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Syllabuses_Sections4_Section4Id",
                table: "Syllabuses",
                column: "Section4Id",
                principalTable: "Sections4",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Syllabuses_Sections5_Section5Id",
                table: "Syllabuses",
                column: "Section5Id",
                principalTable: "Sections5",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Syllabuses_Sections6_Section6Id",
                table: "Syllabuses",
                column: "Section6Id",
                principalTable: "Sections6",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Syllabuses_Sections7_Section7Id",
                table: "Syllabuses",
                column: "Section7Id",
                principalTable: "Sections7",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Syllabuses_Sections8_Section8Id",
                table: "Syllabuses",
                column: "Section8Id",
                principalTable: "Sections8",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Syllabuses_Sections9_Section9Id",
                table: "Syllabuses",
                column: "Section9Id",
                principalTable: "Sections9",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Sections3_Section3Id",
                table: "Syllabuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Sections4_Section4Id",
                table: "Syllabuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Sections5_Section5Id",
                table: "Syllabuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Sections6_Section6Id",
                table: "Syllabuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Sections7_Section7Id",
                table: "Syllabuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Sections8_Section8Id",
                table: "Syllabuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Sections9_Section9Id",
                table: "Syllabuses");

            migrationBuilder.DropTable(
                name: "Section4Subjects");

            migrationBuilder.DropTable(
                name: "Section8Elements");

            migrationBuilder.DropTable(
                name: "Sections4");

            migrationBuilder.DropTable(
                name: "Sections5");

            migrationBuilder.DropTable(
                name: "Sections6");

            migrationBuilder.DropTable(
                name: "Sections7");

            migrationBuilder.DropTable(
                name: "Sections9");

            migrationBuilder.DropTable(
                name: "Sections8");

            migrationBuilder.DropIndex(
                name: "IX_Syllabuses_Section3Id",
                table: "Syllabuses");

            migrationBuilder.DropIndex(
                name: "IX_Syllabuses_Section4Id",
                table: "Syllabuses");

            migrationBuilder.DropIndex(
                name: "IX_Syllabuses_Section5Id",
                table: "Syllabuses");

            migrationBuilder.DropIndex(
                name: "IX_Syllabuses_Section6Id",
                table: "Syllabuses");

            migrationBuilder.DropIndex(
                name: "IX_Syllabuses_Section7Id",
                table: "Syllabuses");

            migrationBuilder.DropIndex(
                name: "IX_Syllabuses_Section8Id",
                table: "Syllabuses");

            migrationBuilder.DropIndex(
                name: "IX_Syllabuses_Section9Id",
                table: "Syllabuses");

            migrationBuilder.DropColumn(
                name: "Section3Id",
                table: "Syllabuses");

            migrationBuilder.DropColumn(
                name: "Section4Id",
                table: "Syllabuses");

            migrationBuilder.DropColumn(
                name: "Section5Id",
                table: "Syllabuses");

            migrationBuilder.DropColumn(
                name: "Section6Id",
                table: "Syllabuses");

            migrationBuilder.DropColumn(
                name: "Section7Id",
                table: "Syllabuses");

            migrationBuilder.DropColumn(
                name: "Section8Id",
                table: "Syllabuses");

            migrationBuilder.DropColumn(
                name: "Section9Id",
                table: "Syllabuses");
        }
    }
}
