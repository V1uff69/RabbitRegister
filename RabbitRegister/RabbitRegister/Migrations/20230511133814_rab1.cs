using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RabbitRegister.Migrations
{
    /// <inheritdoc />
    public partial class rab1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Breeder",
                columns: table => new
                {
                    BreederRegNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breeder", x => x.BreederRegNo);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BreederRegNo = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Rabbits",
                columns: table => new
                {
                    RabbitRegNo = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Race = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    DeadOrAlive = table.Column<int>(type: "int", nullable: false),
                    IsForSale = table.Column<int>(type: "int", nullable: false),
                    SuitableForBreeding = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CauseOfDeath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rabbits", x => x.RabbitRegNo);
                });

            migrationBuilder.CreateTable(
                name: "Trimmings",
                columns: table => new
                {
                    TrimmingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RabbitRegNo = table.Column<int>(type: "int", nullable: false),
                    BreederRegNo = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeUsed = table.Column<double>(type: "float", nullable: true),
                    HairLengthByDayNinety = table.Column<double>(type: "float", nullable: true),
                    WoolDensity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstSortmentWeight = table.Column<double>(type: "float", nullable: false),
                    SecondSortmentWeight = table.Column<double>(type: "float", nullable: false),
                    DisposableWoolWeight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trimmings", x => x.TrimmingId);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    RecipientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    ZipCode = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wools",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Quality = table.Column<int>(type: "int", nullable: false),
                    ImgString = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wools", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Wools_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Yarns",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Fiber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NeedleSize = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Tension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Washing = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yarns", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Yarns_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_ProductId",
                table: "Order",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Breeder");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Rabbits");

            migrationBuilder.DropTable(
                name: "Trimmings");

            migrationBuilder.DropTable(
                name: "Wools");

            migrationBuilder.DropTable(
                name: "Yarns");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
