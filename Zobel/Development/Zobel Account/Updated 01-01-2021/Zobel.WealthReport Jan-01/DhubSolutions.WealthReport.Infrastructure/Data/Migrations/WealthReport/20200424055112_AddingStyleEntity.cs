using Microsoft.EntityFrameworkCore.Migrations;

namespace Zobel.WealthReport.Infrastructure.Data.Migrations.WealthReport
{
    public partial class AddingStyleEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Style",
                schema: "gral",
                columns: table => new
                {
                    StyleID = table.Column<string>(maxLength: 100, nullable: false),
                    StyleName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Style", x => x.StyleID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Style",
                schema: "market",
                table: "Product",
                column: "Style");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Style",
                schema: "market",
                table: "Product",
                column: "Style",
                principalSchema: "gral",
                principalTable: "Style",
                principalColumn: "StyleID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Style",
                schema: "market",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Style",
                schema: "gral");

            migrationBuilder.DropIndex(
                name: "IX_Product_Style",
                schema: "market",
                table: "Product");
        }
    }
}
