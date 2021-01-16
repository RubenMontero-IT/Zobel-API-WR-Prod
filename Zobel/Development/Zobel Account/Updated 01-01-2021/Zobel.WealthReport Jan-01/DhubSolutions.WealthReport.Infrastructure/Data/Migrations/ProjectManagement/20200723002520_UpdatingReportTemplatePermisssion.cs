using Microsoft.EntityFrameworkCore.Migrations;

namespace Zobel.WealthReport.Infrastructure.Data.Migrations.ProjectManagement
{
    public partial class UpdatingReportTemplatePermisssion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportTemplatePermission_Permission",
                schema: "wrp",
                table: "ReportTemplatePermission");

            migrationBuilder.DropIndex(
                name: "IX_ReportTemplatePermission",
                schema: "wrp",
                table: "ReportTemplatePermission");

            migrationBuilder.AlterColumn<string>(
                name: "PermissionID",
                schema: "wrp",
                table: "ReportTemplatePermission",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            #region
            //migrationBuilder.AddColumn<string>(
            //    name: "Code",
            //    schema: "wrp",
            //    table: "ReportTemplate",
            //    nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "StatementCategory",
            //    schema: "wrp",
            //    columns: table => new
            //    {
            //        StatementCategoryID = table.Column<string>(maxLength: 100, nullable: false),
            //        StatementCategoryName = table.Column<string>(maxLength: 100, nullable: true),
            //        Description = table.Column<string>(maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_StatementCategory", x => x.StatementCategoryID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TemplateSettings",
            //    schema: "wrp",
            //    columns: table => new
            //    {
            //        ID = table.Column<string>(maxLength: 100, nullable: false),
            //        UserRoleOrgID = table.Column<string>(nullable: false),
            //        ORGID = table.Column<string>(nullable: false),
            //        TemplateCode = table.Column<string>(nullable: false),
            //        Settings = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TemplateSettings", x => x.ID);
            //        table.ForeignKey(
            //            name: "FK_TemplateSettings_Organization",
            //            column: x => x.ORGID,
            //            principalSchema: "userm",
            //            principalTable: "Organization",
            //            principalColumn: "ORGID",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_TemplateSettings_ReportTemplate",
            //            column: x => x.TemplateCode,
            //            principalSchema: "wrp",
            //            principalTable: "ReportTemplate",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_TemplateSettings_UserRoleOrg",
            //            column: x => x.UserRoleOrgID,
            //            principalSchema: "userm",
            //            principalTable: "UserRoleOrg",
            //            principalColumn: "UserRoleOrgID",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SignableStatement",
            //    schema: "wrp",
            //    columns: table => new
            //    {
            //        SignableStatementID = table.Column<string>(maxLength: 100, nullable: false),
            //        StatementCategoryID = table.Column<string>(maxLength: 100, nullable: true),
            //        OrganizationID = table.Column<string>(maxLength: 100, nullable: true),
            //        TemplateID = table.Column<string>(maxLength: 100, nullable: true),
            //        SignedBy = table.Column<string>(maxLength: 100, nullable: true),
            //        SignedDate = table.Column<DateTime>(type: "datetime", nullable: true),
            //        Content = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SignableStatement", x => x.SignableStatementID);
            //        table.ForeignKey(
            //            name: "FK_SignableStatement_Organization",
            //            column: x => x.OrganizationID,
            //            principalSchema: "userm",
            //            principalTable: "Organization",
            //            principalColumn: "ORGID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_SignableStatement_ReportTemplate",
            //            column: x => x.TemplateID,
            //            principalSchema: "wrp",
            //            principalTable: "ReportTemplate",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_SignableStatement_User",
            //            column: x => x.SignedBy,
            //            principalSchema: "userm",
            //            principalTable: "User",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_SignableStatement_StatementCategory",
            //            column: x => x.StatementCategoryID,
            //            principalSchema: "wrp",
            //            principalTable: "StatementCategory",
            //            principalColumn: "StatementCategoryID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            #endregion

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplatePermission_OrganizationRoleID",
                schema: "wrp",
                table: "ReportTemplatePermission",
                column: "OrganizationRoleID");

            #region 

            //migrationBuilder.CreateIndex(
            //    name: "IX_SignableStatement_OrganizationID",
            //    schema: "wrp",
            //    table: "SignableStatement",
            //    column: "OrganizationID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SignableStatement_TemplateID",
            //    schema: "wrp",
            //    table: "SignableStatement",
            //    column: "TemplateID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SignableStatement_SignedBy",
            //    schema: "wrp",
            //    table: "SignableStatement",
            //    column: "SignedBy");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SignableStatement_StatementCategoryID",
            //    schema: "wrp",
            //    table: "SignableStatement",
            //    column: "StatementCategoryID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TemplateSettings_ORGID",
            //    schema: "wrp",
            //    table: "TemplateSettings",
            //    column: "ORGID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TemplateSettings_TemplateCode",
            //    schema: "wrp",
            //    table: "TemplateSettings",
            //    column: "TemplateCode");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TemplateSettings_UserRoleOrgID",
            //    schema: "wrp",
            //    table: "TemplateSettings",
            //    column: "UserRoleOrgID");

            #endregion

            migrationBuilder.AddForeignKey(
                name: "FK_ReportTemplatePermission_Permission",
                schema: "wrp",
                table: "ReportTemplatePermission",
                column: "PermissionID",
                principalSchema: "userm",
                principalTable: "Permission",
                principalColumn: "PermissionID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportTemplatePermission_Permission",
                schema: "wrp",
                table: "ReportTemplatePermission");

            //migrationBuilder.DropTable(
            //    name: "SignableStatement",
            //    schema: "wrp");

            //migrationBuilder.DropTable(
            //    name: "TemplateSettings",
            //    schema: "wrp");

            //migrationBuilder.DropTable(
            //    name: "StatementCategory",
            //    schema: "wrp");

            //migrationBuilder.DropIndex(
            //    name: "IX_ReportTemplatePermission_OrganizationRoleID",
            //    schema: "wrp",
            //    table: "ReportTemplatePermission");

            //migrationBuilder.DropColumn(
            //    name: "Code",
            //    schema: "wrp",
            //    table: "ReportTemplate");

            migrationBuilder.AlterColumn<string>(
                name: "PermissionID",
                schema: "wrp",
                table: "ReportTemplatePermission",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportTemplatePermission",
                schema: "wrp",
                table: "ReportTemplatePermission",
                columns: new[] { "OrganizationRoleID", "PermissionID", "ReportTemplateID" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportTemplatePermission_Permission",
                schema: "wrp",
                table: "ReportTemplatePermission",
                column: "PermissionID",
                principalSchema: "userm",
                principalTable: "Permission",
                principalColumn: "PermissionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
