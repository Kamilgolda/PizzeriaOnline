using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzeriaOnline.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    HasDelivery = table.Column<bool>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Availability = table.Column<bool>(type: "INTEGER", nullable: false),
                    ImageName = table.Column<string>(type: "TEXT", nullable: true),
                    ImageMimeType = table.Column<string>(type: "TEXT", nullable: true),
                    PhotoFile = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComponentsProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComponentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentsProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentsProducts_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentsProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PricesForSizesProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Size = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricesForSizesProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricesForSizesProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsInOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsInOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsInOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsInOrders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "sos pomidorowy" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "ser mozzarella" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Address", "HasDelivery", "LastName", "Name", "PhoneNumber", "Price", "Status" },
                values: new object[] { 1, "Lubenia", true, "Golda", "Kamil", "789456123", 555.0, 0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Availability", "ImageMimeType", "ImageName", "PhotoFile", "Title" },
                values: new object[] { 1, true, "image/jpeg", "margherita.jpg", null, "MARGHERITA" });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 2, 2, 1 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 1, 13.5, 1, 0 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 2, 18.0, 1, 1 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 3, 28.0, 1, 2 });

            migrationBuilder.InsertData(
                table: "ProductsInOrders",
                columns: new[] { "Id", "OrderId", "ProductId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentsProducts_ComponentId",
                table: "ComponentsProducts",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentsProducts_ProductId",
                table: "ComponentsProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PricesForSizesProducts_ProductId",
                table: "PricesForSizesProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInOrders_OrderId",
                table: "ProductsInOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInOrders_ProductId",
                table: "ProductsInOrders",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentsProducts");

            migrationBuilder.DropTable(
                name: "PricesForSizesProducts");

            migrationBuilder.DropTable(
                name: "ProductsInOrders");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
