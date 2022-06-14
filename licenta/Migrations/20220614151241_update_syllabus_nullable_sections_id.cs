using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace licenta.Migrations
{
    public partial class update_syllabus_nullable_sections_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Sections1_Section1Id",
                table: "Syllabuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Sections2_Section2Id",
                table: "Syllabuses");

            migrationBuilder.AlterColumn<Guid>(
                name: "Section2Id",
                table: "Syllabuses",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "Section1Id",
                table: "Syllabuses",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Syllabuses_Sections1_Section1Id",
                table: "Syllabuses",
                column: "Section1Id",
                principalTable: "Sections1",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Syllabuses_Sections2_Section2Id",
                table: "Syllabuses",
                column: "Section2Id",
                principalTable: "Sections2",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Sections1_Section1Id",
                table: "Syllabuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Syllabuses_Sections2_Section2Id",
                table: "Syllabuses");

            migrationBuilder.AlterColumn<Guid>(
                name: "Section2Id",
                table: "Syllabuses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Section1Id",
                table: "Syllabuses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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
    }
}
