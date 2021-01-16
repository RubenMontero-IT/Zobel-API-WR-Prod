using Microsoft.EntityFrameworkCore.Migrations;

namespace Zobel.WealthReport.Infrastructure.Data.Migrations.ProjectManagement
{
    public partial class AddingConnectionByOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConnectionByOrganization",
                schema: "wrp",
                columns: table => new
                {
                    ORGID = table.Column<string>(maxLength: 100, nullable: false),
                    ConnectionID = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionByOrganization", x => x.ORGID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectionByOrganization",
                schema: "wrp");
        }
    }
}
