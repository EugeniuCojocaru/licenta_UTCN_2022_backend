using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace licenta.Migrations
{
    public partial class update_section4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Section4Subjects");

            migrationBuilder.CreateTable(
                name: "SyllabusSubjects",
                columns: table => new
                {
                    SyllabusId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyllabusSubjects", x => new { x.SubjectId, x.SyllabusId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SyllabusSubjects");

            migrationBuilder.CreateTable(
                name: "Section4Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Section4Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section4Subjects", x => new { x.SubjectId, x.Section4Id });
                });
        }
    }
}
