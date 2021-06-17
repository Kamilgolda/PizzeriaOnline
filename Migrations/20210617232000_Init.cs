using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzeriaOnline.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
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
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Size = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "sos pomidorowy" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 17, "rukola" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 16, "szczypiorek" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 15, "pomidor suszony" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 14, "sos czosnkowy" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 13, "kurczak" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 11, "ser pleśniowy" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 10, "ser wędzony" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 12, "parmezan" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 8, "czarne oliwki" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 7, "pomidorki koktajlowe" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "salami" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "kukurydza" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "pieczarki" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "szynka" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "ser mozzarella" });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Id", "Name" },
                values: new object[] { 9, "cebula czerwona" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Availability", "ImageMimeType", "ImageName", "PhotoFile", "Title" },
                values: new object[] { 6, true, "image/jpeg", "palermo.jpg", null, "PALERMO" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Availability", "ImageMimeType", "ImageName", "PhotoFile", "Title" },
                values: new object[] { 1, true, "image/jpeg", "margherita.jpg", null, "MARGHERITA" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Availability", "ImageMimeType", "ImageName", "PhotoFile", "Title" },
                values: new object[] { 2, true, "image/jpeg", "vesuvio.jpg", null, "VESUVIO" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Availability", "ImageMimeType", "ImageName", "PhotoFile", "Title" },
                values: new object[] { 3, true, "image/jpeg", "salami.jpg", null, "SALAMI" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Availability", "ImageMimeType", "ImageName", "PhotoFile", "Title" },
                values: new object[] { 4, true, "image/jpeg", "formaggi.jpg", null, "QUATTRO FROMAGGI" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Availability", "ImageMimeType", "ImageName", "PhotoFile", "Title" },
                values: new object[] { 5, true, "image/jpeg", "la_nonna.jpg", null, "LA NONNA" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Availability", "ImageMimeType", "ImageName", "PhotoFile", "Title" },
                values: new object[] { 7, true, "image/jpeg", "italiana.jpg", null, "ITALIANA" });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 29, 14, 7 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 14, 1, 4 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 15, 2, 4 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 16, 10, 4 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 17, 11, 4 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 18, 12, 4 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 25, 2, 6 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 28, 7, 6 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 19, 1, 5 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 20, 2, 5 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 21, 13, 5 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 22, 4, 5 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 23, 5, 5 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 26, 6, 6 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 27, 11, 6 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 12, 8, 3 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 13, 9, 3 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 10, 6, 3 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 2, 2, 1 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 34, 17, 7 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 33, 16, 7 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 3, 1, 2 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 4, 2, 2 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 5, 3, 2 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 6, 4, 2 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 11, 7, 3 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 24, 1, 6 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 32, 9, 7 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 31, 15, 7 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 30, 13, 7 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 8, 1, 3 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 9, 2, 3 });

            migrationBuilder.InsertData(
                table: "ComponentsProducts",
                columns: new[] { "Id", "ComponentId", "ProductId" },
                values: new object[] { 7, 5, 2 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 17, 26.0, 6, 1 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 18, 35.0, 6, 2 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 19, 19.0, 7, 0 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 16, 18.0, 6, 0 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 10, 17.0, 4, 0 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 14, 23.0, 5, 1 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 13, 15.0, 5, 0 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 12, 35.0, 4, 2 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 11, 25.0, 4, 1 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 20, 26.0, 7, 1 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 9, 33.0, 3, 2 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 8, 24.0, 3, 1 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 7, 16.0, 3, 0 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 6, 32.0, 2, 2 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 5, 24.0, 2, 1 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 4, 15.0, 2, 0 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 3, 26.0, 1, 2 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 2, 18.0, 1, 1 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 1, 13.5, 1, 0 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 15, 30.0, 5, 2 });

            migrationBuilder.InsertData(
                table: "PricesForSizesProducts",
                columns: new[] { "Id", "Price", "ProductId", "Size" },
                values: new object[] { 21, 37.0, 7, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ComponentsProducts");

            migrationBuilder.DropTable(
                name: "PricesForSizesProducts");

            migrationBuilder.DropTable(
                name: "ProductsInOrders");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
