using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace licenta.Migrations
{
    public partial class create_syllabus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConditionsPromotion",
                table: "Sections10",
                newName: "ConditionPromotion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConditionPromotion",
                table: "Sections10",
                newName: "ConditionsPromotion");
        }
    }
}
