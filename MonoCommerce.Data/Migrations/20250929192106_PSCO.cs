using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonoCommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class PSCO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CargoCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    WeightType = table.Column<string>(type: "TEXT", nullable: false),
                    ApiType = table.Column<string>(type: "TEXT", nullable: true),
                    ApiUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CarrierCargoUrlId = table.Column<string>(type: "TEXT", nullable: true),
                    ApiUsername = table.Column<string>(type: "TEXT", nullable: true),
                    ApiPassword = table.Column<string>(type: "TEXT", nullable: true),
                    ApiToken = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ShortDescription = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Barcode = table.Column<string>(type: "TEXT", nullable: true),
                    StockCode = table.Column<string>(type: "TEXT", nullable: true),
                    Sku = table.Column<string>(type: "TEXT", nullable: true),
                    StockQuantity = table.Column<int>(type: "INTEGER", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: true),
                    SalePrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    DiscountRate = table.Column<decimal>(type: "TEXT", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    CargoWeightKg = table.Column<decimal>(type: "TEXT", nullable: true),
                    Desi = table.Column<decimal>(type: "TEXT", nullable: true),
                    PromotionSite = table.Column<string>(type: "TEXT", nullable: true),
                    CargoCompanyId = table.Column<int>(type: "INTEGER", nullable: true),
                    ImagePath = table.Column<string>(type: "TEXT", nullable: true),
                    ShowInECommerceTheme = table.Column<bool>(type: "INTEGER", nullable: true),
                    YoutubeLink = table.Column<string>(type: "TEXT", nullable: true),
                    HtmlContent = table.Column<string>(type: "TEXT", nullable: true),
                    SeoTitle = table.Column<string>(type: "TEXT", nullable: true),
                    SeoDescription = table.Column<string>(type: "TEXT", nullable: true),
                    SeoKeywords = table.Column<string>(type: "TEXT", nullable: true),
                    Tags = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_CargoCompanies_CargoCompanyId",
                        column: x => x.CargoCompanyId,
                        principalTable: "CargoCompanies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Prices = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentMethod = table.Column<string>(type: "TEXT", nullable: false),
                    Currency = table.Column<string>(type: "TEXT", nullable: false),
                    WhatsappEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    WhatsappAvailableTime = table.Column<string>(type: "TEXT", nullable: true),
                    WhatsappPhone = table.Column<string>(type: "TEXT", nullable: true),
                    SmsApproval = table.Column<bool>(type: "INTEGER", nullable: false),
                    FtpCreated = table.Column<bool>(type: "INTEGER", nullable: false),
                    Theme = table.Column<string>(type: "TEXT", nullable: false),
                    PixelCode = table.Column<string>(type: "TEXT", nullable: true),
                    PixelSpecialCode = table.Column<string>(type: "TEXT", nullable: true),
                    PixelIds = table.Column<string>(type: "TEXT", nullable: true),
                    CounterCode = table.Column<string>(type: "TEXT", nullable: true),
                    HtmlContent = table.Column<string>(type: "TEXT", nullable: true),
                    FormHtml = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sites_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerPhone = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerEmail = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    SiteId = table.Column<int>(type: "INTEGER", nullable: true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: true),
                    IsConfirmedByPhone = table.Column<bool>(type: "INTEGER", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ConfirmedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ShippedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeliveredDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SiteId",
                table: "Orders",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CargoCompanyId",
                table: "Products",
                column: "CargoCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Sites_ProductId",
                table: "Sites",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "CargoCompanies");
        }
    }
}
