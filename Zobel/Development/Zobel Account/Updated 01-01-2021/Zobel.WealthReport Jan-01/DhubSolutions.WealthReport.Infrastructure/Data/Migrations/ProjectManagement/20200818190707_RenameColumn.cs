using Microsoft.EntityFrameworkCore.Migrations;

namespace Zobel.WealthReport.Infrastructure.Data.Migrations.ProjectManagement
{
    public partial class RenameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "wrp",
                table: "ReportTemplateOrganizations",
                newName: "ReportTemplateOrganizationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReportTemplateOrganizationID",
                schema: "wrp",
                table: "ReportTemplateOrganizations",
                newName: "ID");
        }
    }
}
