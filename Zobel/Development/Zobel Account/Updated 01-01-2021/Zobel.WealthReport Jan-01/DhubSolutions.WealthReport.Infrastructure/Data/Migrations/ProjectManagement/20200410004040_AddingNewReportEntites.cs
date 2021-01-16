using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zobel.WealthReport.Infrastructure.Data.Migrations.ProjectManagement
{
    public partial class AddingNewReportEntites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.EnsureSchema(
                name: "wrp");

           
            migrationBuilder.CreateTable(
                name: "ReportTemplate",
                schema: "wrp",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Metadata = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Visibility = table.Column<string>(maxLength: 100, nullable: true),
                    Data = table.Column<string>(nullable: true),
                    CreatedById = table.Column<string>(maxLength: 100, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTemplate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReportTemplate_User",
                        column: x => x.CreatedById,
                        principalSchema: "userm",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            

            migrationBuilder.CreateTable(
                name: "Report",
                schema: "wrp",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Metadata = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Visibility = table.Column<string>(maxLength: 100, nullable: true),
                    Data = table.Column<string>(nullable: true),
                    CreatedById = table.Column<string>(maxLength: 100, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: false),
                    PeriodID = table.Column<string>(maxLength: 7, nullable: false),
                    TemplateId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Report_User",
                        column: x => x.CreatedById,
                        principalSchema: "userm",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Report_ReportTemplate",
                        column: x => x.TemplateId,
                        principalSchema: "wrp",
                        principalTable: "ReportTemplate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ReportTemplateActivePeriod",
                schema: "wrp",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 100, nullable: false),
                    Period = table.Column<string>(maxLength: 7, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ReportTemplateId = table.Column<string>(maxLength: 100, nullable: false),
                    OrganizationId = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTemplateActivePeriod", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReportTemplateActivePeriod_Organization",
                        column: x => x.OrganizationId,
                        principalSchema: "userm",
                        principalTable: "Organization",
                        principalColumn: "ORGID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportTemplateActivePeriod_ReportTemplate",
                        column: x => x.ReportTemplateId,
                        principalSchema: "wrp",
                        principalTable: "ReportTemplate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportTemplateElement",
                schema: "wrp",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Type = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedById = table.Column<string>(maxLength: 100, nullable: true),
                    LastModifiedById = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataIndex = table.Column<string>(nullable: true),
                    Config = table.Column<string>(nullable: true),
                    ElementIndex = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReportTemplateId = table.Column<string>(nullable: true),
                    ContainerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTemplateElement", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReportTemplatetElement_ReportTemplateElement",
                        column: x => x.ContainerId,
                        principalSchema: "wrp",
                        principalTable: "ReportTemplateElement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportTemplateElement_User_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "userm",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportTemplateElement_User_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalSchema: "userm",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportTemplateElement_ReportTemplate",
                        column: x => x.ReportTemplateId,
                        principalSchema: "wrp",
                        principalTable: "ReportTemplate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportTemplatePermission",
                schema: "wrp",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 100, nullable: false),
                    OrganizationRoleID = table.Column<string>(maxLength: 100, nullable: false),
                    PermissionID = table.Column<string>(maxLength: 100, nullable: false),
                    ReportTemplateID = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTemplatePermission", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReportTemplatePermission_OrganizationRole",
                        column: x => x.OrganizationRoleID,
                        principalSchema: "userm",
                        principalTable: "OrganizationRole",
                        principalColumn: "OrganizationRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportTemplatePermission_Permission",
                        column: x => x.PermissionID,
                        principalSchema: "userm",
                        principalTable: "Permission",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportTemplatePermission_ReportTemplate",
                        column: x => x.ReportTemplateID,
                        principalSchema: "wrp",
                        principalTable: "ReportTemplate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportPermission",
                schema: "wrp",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 100, nullable: false),
                    OrganizationRoleID = table.Column<string>(maxLength: 100, nullable: false),
                    PermissionID = table.Column<string>(maxLength: 100, nullable: true),
                    ReportID = table.Column<string>(maxLength: 100, nullable: false),
                    OrganizationID = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportPermission", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReportPermission_Organization",
                        column: x => x.OrganizationID,
                        principalSchema: "userm",
                        principalTable: "Organization",
                        principalColumn: "ORGID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportPermission_OrganizationRole",
                        column: x => x.OrganizationRoleID,
                        principalSchema: "userm",
                        principalTable: "OrganizationRole",
                        principalColumn: "OrganizationRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportPermission_Permission",
                        column: x => x.PermissionID,
                        principalSchema: "userm",
                        principalTable: "Permission",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportPermission_Report",
                        column: x => x.ReportID,
                        principalSchema: "wrp",
                        principalTable: "Report",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportElement",
                schema: "wrp",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Type = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedById = table.Column<string>(maxLength: 100, nullable: true),
                    LastModifiedById = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataIndex = table.Column<string>(nullable: true),
                    Config = table.Column<string>(nullable: true),
                    ReportId = table.Column<string>(nullable: true),
                    ReportTemplateElementId = table.Column<string>(nullable: true),
                    ContainerID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportElement", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReportElement_ReportElement",
                        column: x => x.ContainerID,
                        principalSchema: "wrp",
                        principalTable: "ReportElement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportElement_User_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "userm",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportElement_User_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalSchema: "userm",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportElement_Report",
                        column: x => x.ReportId,
                        principalSchema: "wrp",
                        principalTable: "Report",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportElement_ReportTemplateElement",
                        column: x => x.ReportTemplateElementId,
                        principalSchema: "wrp",
                        principalTable: "ReportTemplateElement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ReportTemplateElementPermission",
                schema: "wrp",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 100, nullable: false),
                    OrganizationRoleID = table.Column<string>(maxLength: 100, nullable: false),
                    PermissionID = table.Column<string>(maxLength: 100, nullable: false),
                    ReportTemplateElementID = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTemplateElementPermission", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReportTemplateElementPermission_OrganizationRole",
                        column: x => x.OrganizationRoleID,
                        principalSchema: "userm",
                        principalTable: "OrganizationRole",
                        principalColumn: "OrganizationRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportTemplateElementPermission_Permission",
                        column: x => x.PermissionID,
                        principalSchema: "userm",
                        principalTable: "Permission",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportTemplateElementPermission_ReportTemplateElement",
                        column: x => x.ReportTemplateElementID,
                        principalSchema: "wrp",
                        principalTable: "ReportTemplateElement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportElementPermission",
                schema: "wrp",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 100, nullable: false),
                    OrganizationRoleID = table.Column<string>(maxLength: 100, nullable: false),
                    PermissionID = table.Column<string>(maxLength: 100, nullable: false),
                    ReportElementID = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportElementPermission", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReportElementPermission_OrganizationRole",
                        column: x => x.OrganizationRoleID,
                        principalSchema: "userm",
                        principalTable: "OrganizationRole",
                        principalColumn: "OrganizationRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportElementPermission_Permission",
                        column: x => x.PermissionID,
                        principalSchema: "userm",
                        principalTable: "Permission",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportElementPermission_ReportElement",
                        column: x => x.ReportElementID,
                        principalSchema: "wrp",
                        principalTable: "ReportElement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

           

            migrationBuilder.CreateIndex(
                name: "IX_Report_CreatedById",
                schema: "wrp",
                table: "Report",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Report_TemplateId",
                schema: "wrp",
                table: "Report",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportElement_ContainerID",
                schema: "wrp",
                table: "ReportElement",
                column: "ContainerID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportElement_CreatedById",
                schema: "wrp",
                table: "ReportElement",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportElement_LastModifiedById",
                schema: "wrp",
                table: "ReportElement",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportElement_ReportId",
                schema: "wrp",
                table: "ReportElement",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportElement_ReportTemplateElementId",
                schema: "wrp",
                table: "ReportElement",
                column: "ReportTemplateElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportElementPermission_PermissionID",
                schema: "wrp",
                table: "ReportElementPermission",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportElementPermission_ReportElementID",
                schema: "wrp",
                table: "ReportElementPermission",
                column: "ReportElementID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportElementPermission",
                schema: "wrp",
                table: "ReportElementPermission",
                columns: new[] { "OrganizationRoleID", "PermissionID", "ReportElementID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportPermission_OrganizationID",
                schema: "wrp",
                table: "ReportPermission",
                column: "OrganizationID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportPermission_PermissionID",
                schema: "wrp",
                table: "ReportPermission",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportPermission_ReportID",
                schema: "wrp",
                table: "ReportPermission",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportPermission",
                schema: "wrp",
                table: "ReportPermission",
                columns: new[] { "OrganizationRoleID", "OrganizationID", "PermissionID", "ReportID" },
                unique: true,
                filter: "[PermissionID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplate_CreatedById",
                schema: "wrp",
                table: "ReportTemplate",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateActivePeriod_OrganizationId",
                schema: "wrp",
                table: "ReportTemplateActivePeriod",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateActivePeriod_ReportTemplateId",
                schema: "wrp",
                table: "ReportTemplateActivePeriod",
                column: "ReportTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateElement_ContainerId",
                schema: "wrp",
                table: "ReportTemplateElement",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateElement_CreatedById",
                schema: "wrp",
                table: "ReportTemplateElement",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateElement_LastModifiedById",
                schema: "wrp",
                table: "ReportTemplateElement",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateElement_ReportTemplateId",
                schema: "wrp",
                table: "ReportTemplateElement",
                column: "ReportTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateElementPermission_PermissionID",
                schema: "wrp",
                table: "ReportTemplateElementPermission",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateElementPermission_ReportTemplateElementID",
                schema: "wrp",
                table: "ReportTemplateElementPermission",
                column: "ReportTemplateElementID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplateElementPermission",
                schema: "wrp",
                table: "ReportTemplateElementPermission",
                columns: new[] { "OrganizationRoleID", "PermissionID", "ReportTemplateElementID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplatePermission_PermissionID",
                schema: "wrp",
                table: "ReportTemplatePermission",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplatePermission_ReportTemplateID",
                schema: "wrp",
                table: "ReportTemplatePermission",
                column: "ReportTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplatePermission",
                schema: "wrp",
                table: "ReportTemplatePermission",
                columns: new[] { "OrganizationRoleID", "PermissionID", "ReportTemplateID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropTable(
                name: "ReportElementPermission",
                schema: "wrp");

            migrationBuilder.DropTable(
                name: "ReportPermission",
                schema: "wrp");

            migrationBuilder.DropTable(
                name: "ReportTemplateActivePeriod",
                schema: "wrp");

            migrationBuilder.DropTable(
                name: "ReportTemplateElementPermission",
                schema: "wrp");

            migrationBuilder.DropTable(
                name: "ReportTemplatePermission",
                schema: "wrp");
           
            migrationBuilder.DropTable(
                name: "ReportElement",
                schema: "wrp");

            migrationBuilder.DropTable(
                name: "OrganizationRole",
                schema: "userm");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "userm");

            migrationBuilder.DropTable(
                name: "Apps",
                schema: "app");

            migrationBuilder.DropTable(
                name: "FileExtension",
                schema: "dbi");

            migrationBuilder.DropTable(
                name: "Report",
                schema: "wrp");

            migrationBuilder.DropTable(
                name: "ReportTemplateElement",
                schema: "wrp");

            migrationBuilder.DropTable(
                name: "Organization",
                schema: "userm");

            migrationBuilder.DropTable(
                name: "RoleValue",
                schema: "userm");

            migrationBuilder.DropTable(
                name: "ReportTemplate",
                schema: "wrp");

            migrationBuilder.DropTable(
                name: "RoleType",
                schema: "userm");

            migrationBuilder.DropTable(
                name: "User",
                schema: "userm");
        }
    }
}
