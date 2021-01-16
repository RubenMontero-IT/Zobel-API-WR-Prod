using Microsoft.EntityFrameworkCore.Migrations;

namespace Zobel.WealthReport.Infrastructure.Data.Migrations.ProjectManagement
{
    public partial class AddCreationOptionsColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreationOptions",
                schema: "wrp",
                table: "Report",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationOptions",
                schema: "wrp",
                table: "Report");
        }
    }
}
