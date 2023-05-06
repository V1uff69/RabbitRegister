using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RabbitRegister.Migrations
{
    /// <inheritdoc />
    public partial class Rab2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trimmings",
                columns: table => new
                {
                    TrimmingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RabbitRegNo = table.Column<int>(type: "int", nullable: false),
                    BreederRegNo = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeUsed = table.Column<double>(type: "float", nullable: false),
                    HairLengthByDayNinety = table.Column<double>(type: "float", nullable: false),
                    WoolDensity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstSortmentWeight = table.Column<double>(type: "float", nullable: false),
                    SecondSortmentWeight = table.Column<double>(type: "float", nullable: false),
                    DisposableWoolWeight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trimmings", x => x.TrimmingId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trimmings");
        }
    }
}
