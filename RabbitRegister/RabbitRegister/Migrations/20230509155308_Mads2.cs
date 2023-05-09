using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RabbitRegister.Migrations
{
    /// <inheritdoc />
    public partial class Mads2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Yarns",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YarnId = table.Column<int>(type: "int", nullable: false),
                    Fiber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NeedleSize = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Tension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Washing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BreederRegNo = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yarns", x => x.ProductId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Yarns");
        }
    }
}
