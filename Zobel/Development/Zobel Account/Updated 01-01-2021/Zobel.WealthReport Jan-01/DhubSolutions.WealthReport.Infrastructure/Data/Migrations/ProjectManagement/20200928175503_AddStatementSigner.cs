using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zobel.WealthReport.Infrastructure.Data.Migrations.ProjectManagement
{
    public partial class AddStatementSigner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SignableStatement_Organization",
                schema: "wrp",
                table: "SignableStatement");

            migrationBuilder.DropForeignKey(
                name: "FK_SignableStatement_ReportTemplate",
                schema: "wrp",
                table: "SignableStatement");

            migrationBuilder.DropForeignKey(
                name: "FK_SignableStatement_User",
                schema: "wrp",
                table: "SignableStatement");

            migrationBuilder.DropForeignKey(
                name: "FK_SignableStatement_StatementCategory",
                schema: "wrp",
                table: "SignableStatement");

            //migrationBuilder.DropIndex(
            //    name: "IX_SignableStatement_SignedBy",
            //    schema: "wrp",
            //    table: "SignableStatement");

            migrationBuilder.DropColumn(
                name: "SignedBy",
                schema: "wrp",
                table: "SignableStatement");

            migrationBuilder.DropColumn(
                name: "SignedDate",
                schema: "wrp",
                table: "SignableStatement");

            migrationBuilder.CreateTable(
                name: "StatementSigner",
                schema: "wrp",
                columns: table => new
                {
                    StatementSignerID = table.Column<string>(maxLength: 100, nullable: false),
                    SignedBy = table.Column<string>(maxLength: 100, nullable: true),
                    SignedDate = table.Column<DateTime>(nullable: true),
                    SignableStatementId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatementSigner", x => x.StatementSignerID);
                    table.ForeignKey(
                        name: "FK_StatementSigner_SignableStatement",
                        column: x => x.SignableStatementId,
                        principalSchema: "wrp",
                        principalTable: "SignableStatement",
                        principalColumn: "SignableStatementID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatementSigner_User",
                        column: x => x.SignedBy,
                        principalSchema: "userm",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatementSigner_SignableStatementId",
                schema: "wrp",
                table: "StatementSigner",
                column: "SignableStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_StatementSigner_SignedBy",
                schema: "wrp",
                table: "StatementSigner",
                column: "SignedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_SignableStatement_Organization",
                schema: "wrp",
                table: "SignableStatement",
                column: "OrganizationID",
                principalSchema: "userm",
                principalTable: "Organization",
                principalColumn: "ORGID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SignableStatement_ReportTemplate",
                schema: "wrp",
                table: "SignableStatement",
                column: "TemplateID",
                principalSchema: "wrp",
                principalTable: "ReportTemplate",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SignableStatement_StatementCategory",
                schema: "wrp",
                table: "SignableStatement",
                column: "StatementCategoryID",
                principalSchema: "wrp",
                principalTable: "StatementCategory",
                principalColumn: "StatementCategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SignableStatement_Organization",
                schema: "wrp",
                table: "SignableStatement");

            migrationBuilder.DropForeignKey(
                name: "FK_SignableStatement_ReportTemplate",
                schema: "wrp",
                table: "SignableStatement");

            migrationBuilder.DropForeignKey(
                name: "FK_SignableStatement_StatementCategory",
                schema: "wrp",
                table: "SignableStatement");

            migrationBuilder.DropTable(
                name: "StatementSigner",
                schema: "wrp");

            migrationBuilder.AddColumn<string>(
                name: "SignedBy",
                schema: "wrp",
                table: "SignableStatement",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SignedDate",
                schema: "wrp",
                table: "SignableStatement",
                type: "datetime",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SignableStatement_SignedBy",
                schema: "wrp",
                table: "SignableStatement",
                column: "SignedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_SignableStatement_Organization",
                schema: "wrp",
                table: "SignableStatement",
                column: "OrganizationID",
                principalSchema: "userm",
                principalTable: "Organization",
                principalColumn: "ORGID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SignableStatement_ReportTemplate",
                schema: "wrp",
                table: "SignableStatement",
                column: "TemplateID",
                principalSchema: "wrp",
                principalTable: "ReportTemplate",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SignableStatement_User",
                schema: "wrp",
                table: "SignableStatement",
                column: "SignedBy",
                principalSchema: "userm",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SignableStatement_StatementCategory",
                schema: "wrp",
                table: "SignableStatement",
                column: "StatementCategoryID",
                principalSchema: "wrp",
                principalTable: "StatementCategory",
                principalColumn: "StatementCategoryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
