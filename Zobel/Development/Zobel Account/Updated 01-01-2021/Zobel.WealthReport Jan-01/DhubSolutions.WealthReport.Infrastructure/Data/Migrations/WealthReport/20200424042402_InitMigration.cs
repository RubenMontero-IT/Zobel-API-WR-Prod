using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zobel.WealthReport.Infrastructure.Data.Migrations.WealthReport
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "gral");

            migrationBuilder.EnsureSchema(
                name: "market");

            migrationBuilder.EnsureSchema(
                name: "invp");

            migrationBuilder.EnsureSchema(
                name: "invpder");

            migrationBuilder.EnsureSchema(
                name: "marketder");

            migrationBuilder.CreateTable(
                name: "Currency",
                schema: "gral",
                columns: table => new
                {
                    CurrencyID = table.Column<string>(type: "char(3)", unicode: false, nullable: false),
                    CurrencyName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.CurrencyID);
                });

            migrationBuilder.CreateTable(
                name: "MetricsCategories",
                schema: "gral",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 100, nullable: false),
                    MetricCode = table.Column<string>(maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 100, nullable: false),
                    Type = table.Column<string>(maxLength: 100, nullable: false),
                    Period = table.Column<string>(maxLength: 100, nullable: false),
                    Plevel = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetricsCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PorfolioCategory",
                schema: "gral",
                columns: table => new
                {
                    PortfolioCategoryID = table.Column<string>(maxLength: 100, nullable: false),
                    PortfolioCategory = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PorfolioCategory", x => x.PortfolioCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                schema: "gral",
                columns: table => new
                {
                    ProductTypeID = table.Column<string>(maxLength: 100, nullable: false),
                    ProductTypeName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.ProductTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                schema: "gral",
                columns: table => new
                {
                    RegionID = table.Column<string>(maxLength: 100, nullable: false),
                    RegionName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.RegionID);
                });

            migrationBuilder.CreateTable(
                name: "Frequency",
                schema: "market",
                columns: table => new
                {
                    FrequencyID = table.Column<string>(maxLength: 100, nullable: false),
                    FrequencyName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequency", x => x.FrequencyID);
                });

            migrationBuilder.CreateTable(
                name: "FXRate",
                schema: "market",
                columns: table => new
                {
                    FXRateID = table.Column<string>(maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    FXRateValue = table.Column<double>(nullable: false),
                    InitialCurrency = table.Column<string>(maxLength: 3, nullable: false),
                    EndCurrency = table.Column<string>(maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FXRate", x => x.FXRateID);
                });

            migrationBuilder.CreateTable(
                name: "Liquidity",
                schema: "market",
                columns: table => new
                {
                    LiquidityID = table.Column<string>(maxLength: 100, nullable: false),
                    LiquidityValue = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liquidity", x => x.LiquidityID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "market",
                columns: table => new
                {
                    ProductID = table.Column<string>(maxLength: 100, nullable: false),
                    SEDOL = table.Column<string>(maxLength: 100, nullable: true),
                    CUSIP = table.Column<string>(maxLength: 100, nullable: true),
                    ISIN = table.Column<string>(maxLength: 100, nullable: false),
                    Ticker = table.Column<string>(maxLength: 100, nullable: true),
                    BloombergID = table.Column<string>(maxLength: 100, nullable: true),
                    BloombergName = table.Column<string>(maxLength: 100, nullable: true),
                    DisplayName = table.Column<string>(maxLength: 150, nullable: false),
                    Type = table.Column<string>(maxLength: 100, nullable: true),
                    Style = table.Column<string>(maxLength: 100, nullable: true),
                    Region = table.Column<string>(maxLength: 100, nullable: true),
                    BaseCurrency = table.Column<string>(type: "char(3)", unicode: false, nullable: true),
                    ContactDetails = table.Column<string>(nullable: true),
                    AUM = table.Column<string>(nullable: true),
                    Employees = table.Column<string>(nullable: true),
                    ManagementFee = table.Column<double>(nullable: true),
                    PeformanceFee = table.Column<double>(nullable: true),
                    TakingBackEffect = table.Column<bool>(nullable: true),
                    Strengh = table.Column<string>(nullable: true),
                    History = table.Column<string>(nullable: true),
                    TotalUnitNumber = table.Column<double>(nullable: false),
                    ManagerName = table.Column<string>(maxLength: 256, nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    Address = table.Column<string>(maxLength: 256, nullable: true),
                    LastHistUpdate = table.Column<DateTime>(type: "date", nullable: true),
                    OtherID = table.Column<string>(maxLength: 256, nullable: true),
                    GeneralDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Product_Currency",
                        column: x => x.BaseCurrency,
                        principalSchema: "gral",
                        principalTable: "Currency",
                        principalColumn: "CurrencyID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Product_Region",
                        column: x => x.Region,
                        principalSchema: "gral",
                        principalTable: "Region",
                        principalColumn: "RegionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_ProductType",
                        column: x => x.Type,
                        principalSchema: "gral",
                        principalTable: "ProductType",
                        principalColumn: "ProductTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationProduct",
                schema: "invp",
                columns: table => new
                {
                    OrgProductID = table.Column<string>(maxLength: 100, nullable: false),
                    ProductID = table.Column<string>(maxLength: 100, nullable: false),
                    OrganizationID = table.Column<string>(maxLength: 100, nullable: false),
                    EntryPrice = table.Column<double>(nullable: true),
                    ExitPrice = table.Column<double>(nullable: true),
                    InitNumberOfUnits = table.Column<double>(nullable: true),
                    InitialInvestmentEUR = table.Column<double>(nullable: true),
                    ExitNumberOfUnits = table.Column<double>(nullable: true),
                    InitialDate = table.Column<DateTime>(type: "date", nullable: true),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    DisplayName = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationProduct", x => x.OrgProductID);
                    table.UniqueConstraint("AK_OrganizationProduct_ProductID_OrganizationID", x => new { x.ProductID, x.OrganizationID });
                    table.ForeignKey(
                        name: "FK_OrganizationProduct_Product",
                        column: x => x.ProductID,
                        principalSchema: "market",
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductRegistry",
                schema: "invp",
                columns: table => new
                {
                    ProductRegistryID = table.Column<string>(maxLength: 100, nullable: false),
                    ProductID = table.Column<string>(maxLength: 100, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRegistry", x => x.ProductRegistryID);
                    table.UniqueConstraint("AK_ProductRegistry_ProductID_Date", x => new { x.ProductID, x.Date });
                    table.ForeignKey(
                        name: "FK_ProductRegistry_Product",
                        column: x => x.ProductID,
                        principalSchema: "market",
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LiquidityProduct",
                schema: "market",
                columns: table => new
                {
                    LiquidityProductID = table.Column<string>(maxLength: 100, nullable: false),
                    ProductID = table.Column<string>(maxLength: 100, nullable: false),
                    LiquidityID = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiquidityProduct", x => x.LiquidityProductID);
                    table.ForeignKey(
                        name: "FK_LiquidityProduct_Liquidity",
                        column: x => x.LiquidityID,
                        principalSchema: "market",
                        principalTable: "Liquidity",
                        principalColumn: "LiquidityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LiquidityProduct_Product",
                        column: x => x.ProductID,
                        principalSchema: "market",
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductFrequency",
                schema: "market",
                columns: table => new
                {
                    ProductFrequencyID = table.Column<string>(maxLength: 100, nullable: false),
                    FrequencyID = table.Column<string>(maxLength: 100, nullable: false),
                    ProductID = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFrequency", x => x.ProductFrequencyID);
                    table.ForeignKey(
                        name: "FK_ProductFrequency_Frequency",
                        column: x => x.FrequencyID,
                        principalSchema: "market",
                        principalTable: "Frequency",
                        principalColumn: "FrequencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductFrequency_Product",
                        column: x => x.ProductID,
                        principalSchema: "market",
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationProductRegistry",
                schema: "invp",
                columns: table => new
                {
                    OrgProductRegistryID = table.Column<string>(maxLength: 100, nullable: false),
                    ProductID = table.Column<string>(maxLength: 100, nullable: false),
                    OrganizationID = table.Column<string>(maxLength: 100, nullable: false),
                    NumberOfUnits = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationProductRegistry", x => x.OrgProductRegistryID);
                    table.UniqueConstraint("AK_OrganizationProductRegistry_ProductID_OrganizationID_Date", x => new { x.ProductID, x.OrganizationID, x.Date });
                    table.ForeignKey(
                        name: "FK_OrganizationProductRegistry_OrganizationProduct",
                        columns: x => new { x.ProductID, x.OrganizationID },
                        principalSchema: "invp",
                        principalTable: "OrganizationProduct",
                        principalColumns: new[] { "ProductID", "OrganizationID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioCatOrgProd",
                schema: "invp",
                columns: table => new
                {
                    PortfolioCatOrgProdID = table.Column<string>(maxLength: 100, nullable: false),
                    PortfolioCategoryID = table.Column<string>(maxLength: 100, nullable: false),
                    ProductID = table.Column<string>(maxLength: 100, nullable: false),
                    OrganizationID = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioCatOrgProd", x => x.PortfolioCatOrgProdID);
                    table.ForeignKey(
                        name: "FK_PortfolioCatOrgProd_PorfolioCategory",
                        column: x => x.PortfolioCategoryID,
                        principalSchema: "gral",
                        principalTable: "PorfolioCategory",
                        principalColumn: "PortfolioCategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PortfolioCatOrgProd_OrganizationProduct",
                        columns: x => new { x.ProductID, x.OrganizationID },
                        principalSchema: "invp",
                        principalTable: "OrganizationProduct",
                        principalColumns: new[] { "ProductID", "OrganizationID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductExtendedRegistry",
                schema: "marketder",
                columns: table => new
                {
                    ProdExtendedRegistryID = table.Column<string>(maxLength: 100, nullable: false),
                    ProductID = table.Column<string>(maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    MainCurrency = table.Column<string>(type: "char(3)", unicode: false, nullable: false),
                    SEDOL = table.Column<string>(maxLength: 100, nullable: true),
                    CUSIP = table.Column<string>(maxLength: 100, nullable: true),
                    ISIN = table.Column<string>(maxLength: 100, nullable: false),
                    Ticker = table.Column<string>(maxLength: 100, nullable: true),
                    BloombergID = table.Column<string>(maxLength: 100, nullable: true),
                    Price = table.Column<double>(nullable: false),
                    BaseCurrency = table.Column<string>(type: "char(3)", unicode: false, nullable: false),
                    Vol = table.Column<double>(nullable: true),
                    ITDVol = table.Column<double>(nullable: true),
                    RoR = table.Column<double>(nullable: true),
                    VAMI = table.Column<double>(nullable: true),
                    ITDRoR = table.Column<double>(nullable: true),
                    NAV = table.Column<double>(nullable: true),
                    PL = table.Column<double>(nullable: true),
                    ITDPL = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductExtendedRegistry", x => x.ProdExtendedRegistryID);
                    table.ForeignKey(
                        name: "FK_ProductExtendedRegistry_Currency1",
                        column: x => x.BaseCurrency,
                        principalSchema: "gral",
                        principalTable: "Currency",
                        principalColumn: "CurrencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductExtendedRegistry_Currency",
                        column: x => x.MainCurrency,
                        principalSchema: "gral",
                        principalTable: "Currency",
                        principalColumn: "CurrencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductExtendedRegistry_ProductRegistry",
                        columns: x => new { x.ProductID, x.Date },
                        principalSchema: "invp",
                        principalTable: "ProductRegistry",
                        principalColumns: new[] { "ProductID", "Date" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationProductExtendedRegistry",
                schema: "invpder",
                columns: table => new
                {
                    OrgProductExtendedRegistryID = table.Column<string>(maxLength: 100, nullable: false),
                    ProductID = table.Column<string>(maxLength: 100, nullable: false),
                    ISIN = table.Column<string>(maxLength: 100, nullable: false),
                    SEDOL = table.Column<string>(maxLength: 100, nullable: true),
                    CUSIP = table.Column<string>(maxLength: 100, nullable: false),
                    Ticker = table.Column<string>(maxLength: 100, nullable: true),
                    BaseCurrency = table.Column<string>(type: "char(3)", unicode: false, nullable: true),
                    BloombergID = table.Column<string>(maxLength: 100, nullable: true),
                    OrganizationID = table.Column<string>(maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    RoR = table.Column<double>(nullable: true),
                    ITDRoR = table.Column<string>(maxLength: 10, nullable: true),
                    ITDPL = table.Column<string>(maxLength: 10, nullable: true),
                    PL = table.Column<double>(nullable: true),
                    PLFX = table.Column<double>(nullable: true),
                    ITDPLFX = table.Column<string>(maxLength: 10, nullable: true),
                    Vol = table.Column<string>(maxLength: 10, nullable: true),
                    ITDVol = table.Column<string>(maxLength: 10, nullable: true),
                    VAMI = table.Column<string>(maxLength: 10, nullable: true),
                    NAV = table.Column<double>(nullable: true),
                    MainCurrency = table.Column<string>(type: "char(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationProductExtendedRegistry", x => x.OrgProductExtendedRegistryID);
                    table.ForeignKey(
                        name: "FK_OrganizationProductExtendedRegistry_Currency",
                        column: x => x.BaseCurrency,
                        principalSchema: "gral",
                        principalTable: "Currency",
                        principalColumn: "CurrencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationProductExtendedRegistry_Currency1",
                        column: x => x.MainCurrency,
                        principalSchema: "gral",
                        principalTable: "Currency",
                        principalColumn: "CurrencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationProductExtendedRegistry_OrganizationProductRegistry",
                        columns: x => new { x.ProductID, x.OrganizationID, x.Date },
                        principalSchema: "invp",
                        principalTable: "OrganizationProductRegistry",
                        principalColumns: new[] { "ProductID", "OrganizationID", "Date" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationProduct",
                schema: "invp",
                table: "OrganizationProduct",
                columns: new[] { "ProductID", "OrganizationID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationProductRegistry",
                schema: "invp",
                table: "OrganizationProductRegistry",
                columns: new[] { "ProductID", "OrganizationID", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioCatOrgProd_ProductID_OrganizationID",
                schema: "invp",
                table: "PortfolioCatOrgProd",
                columns: new[] { "ProductID", "OrganizationID" });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioCatOrgProd",
                schema: "invp",
                table: "PortfolioCatOrgProd",
                columns: new[] { "PortfolioCategoryID", "ProductID", "OrganizationID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductRegistry",
                schema: "invp",
                table: "ProductRegistry",
                columns: new[] { "ProductID", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationProductExtendedRegistry_BaseCurrency",
                schema: "invpder",
                table: "OrganizationProductExtendedRegistry",
                column: "BaseCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationProductExtendedRegistry_MainCurrency",
                schema: "invpder",
                table: "OrganizationProductExtendedRegistry",
                column: "MainCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationProductExtendedRegistry",
                schema: "invpder",
                table: "OrganizationProductExtendedRegistry",
                columns: new[] { "ProductID", "OrganizationID", "Date", "MainCurrency" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FXRate",
                schema: "market",
                table: "FXRate",
                columns: new[] { "Date", "InitialCurrency", "EndCurrency" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LiquidityProduct_LiquidityID",
                schema: "market",
                table: "LiquidityProduct",
                column: "LiquidityID");

            migrationBuilder.CreateIndex(
                name: "IX_LiquidityProduct",
                schema: "market",
                table: "LiquidityProduct",
                columns: new[] { "ProductID", "LiquidityID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_BaseCurrency",
                schema: "market",
                table: "Product",
                column: "BaseCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Region",
                schema: "market",
                table: "Product",
                column: "Region");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Type",
                schema: "market",
                table: "Product",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFrequency_ProductID",
                schema: "market",
                table: "ProductFrequency",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFrequency",
                schema: "market",
                table: "ProductFrequency",
                columns: new[] { "FrequencyID", "ProductID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductExtendedRegistry_BaseCurrency",
                schema: "marketder",
                table: "ProductExtendedRegistry",
                column: "BaseCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_ProductExtendedRegistry_MainCurrency",
                schema: "marketder",
                table: "ProductExtendedRegistry",
                column: "MainCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_ProductExtendedRegistry",
                schema: "marketder",
                table: "ProductExtendedRegistry",
                columns: new[] { "ProductID", "Date", "MainCurrency" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetricsCategories",
                schema: "gral");

            migrationBuilder.DropTable(
                name: "PortfolioCatOrgProd",
                schema: "invp");

            migrationBuilder.DropTable(
                name: "OrganizationProductExtendedRegistry",
                schema: "invpder");

            migrationBuilder.DropTable(
                name: "FXRate",
                schema: "market");

            migrationBuilder.DropTable(
                name: "LiquidityProduct",
                schema: "market");

            migrationBuilder.DropTable(
                name: "ProductFrequency",
                schema: "market");

            migrationBuilder.DropTable(
                name: "ProductExtendedRegistry",
                schema: "marketder");

            migrationBuilder.DropTable(
                name: "PorfolioCategory",
                schema: "gral");

            migrationBuilder.DropTable(
                name: "OrganizationProductRegistry",
                schema: "invp");

            migrationBuilder.DropTable(
                name: "Liquidity",
                schema: "market");

            migrationBuilder.DropTable(
                name: "Frequency",
                schema: "market");

            migrationBuilder.DropTable(
                name: "ProductRegistry",
                schema: "invp");

            migrationBuilder.DropTable(
                name: "OrganizationProduct",
                schema: "invp");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "market");

            migrationBuilder.DropTable(
                name: "Currency",
                schema: "gral");

            migrationBuilder.DropTable(
                name: "Region",
                schema: "gral");

            migrationBuilder.DropTable(
                name: "ProductType",
                schema: "gral");
        }
    }
}
