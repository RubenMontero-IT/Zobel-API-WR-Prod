using Microsoft.EntityFrameworkCore.Migrations;

namespace Zobel.WealthReport.Infrastructure.Data.Migrations.ProjectManagement
{
    public partial class UpdateReportTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportTemplateOrganizations",
                schema: "wrp",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 100, nullable: false),
                    ReportTemplateID = table.Column<string>(maxLength: 100, nullable: false),
                    OrganizationID = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTemplateOrganizations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReportTemplateOrganization_Organization",
                        column: x => x.OrganizationID,
                        principalSchema: "userm",
                        principalTable: "Organization",
                        principalColumn: "ORGID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportTemplateOrganization_ReportTemplate",
                        column: x => x.ReportTemplateID,
                        principalSchema: "wrp",
                        principalTable: "ReportTemplate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateOrganizations_OrganizationID",
                schema: "wrp",
                table: "ReportTemplateOrganizations",
                column: "OrganizationID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateOrganizations_ReportTemplateID",
                schema: "wrp",
                table: "ReportTemplateOrganizations",
                column: "ReportTemplateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportTemplateOrganizations",
                schema: "wrp");
        }
    }
}
