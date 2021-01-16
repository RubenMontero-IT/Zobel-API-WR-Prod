using Microsoft.EntityFrameworkCore.Migrations;

namespace Zobel.WealthReport.Infrastructure.Data.Migrations.ProjectManagement
{
    public partial class AddOrganizationToReportTemplatePermisssion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrganizationID",
                schema: "wrp",
                table: "ReportTemplatePermission",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplatePermission_OrganizationID",
                schema: "wrp",
                table: "ReportTemplatePermission",
                column: "OrganizationID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportTemplatePermission_Organization",
                schema: "wrp",
                table: "ReportTemplatePermission",
                column: "OrganizationID",
                principalSchema: "userm",
                principalTable: "Organization",
                principalColumn: "ORGID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportTemplatePermission_Organization",
                schema: "wrp",
                table: "ReportTemplatePermission");

            migrationBuilder.DropIndex(
                name: "IX_ReportTemplatePermission_OrganizationID",
                schema: "wrp",
                table: "ReportTemplatePermission");

            migrationBuilder.DropColumn(
                name: "OrganizationID",
                schema: "wrp",
                table: "ReportTemplatePermission");
        }
    }
}
