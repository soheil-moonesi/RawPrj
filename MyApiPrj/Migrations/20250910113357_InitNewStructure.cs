using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApiPrj.Migrations
{
    /// <inheritdoc />
    public partial class InitNewStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manufactures",
                columns: table => new
                {
                    ManufacturerIdentifier = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ManufactureName = table.Column<string>(type: "TEXT", nullable: false),
                    ManufactureCountry = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufactures", x => x.ManufacturerIdentifier);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductIdentifier = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ProductQuantityInStock = table.Column<int>(type: "INTEGER", nullable: false),
                    ManufactureTraceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductIdentifier);
                    table.ForeignKey(
                        name: "FK_Products_Manufactures_ManufactureTraceId",
                        column: x => x.ManufactureTraceId,
                        principalTable: "Manufactures",
                        principalColumn: "ManufacturerIdentifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Manufactures",
                columns: new[] { "ManufacturerIdentifier", "ManufactureCountry", "ManufactureName" },
                values: new object[] { 1, "Germany", "Benz" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductIdentifier", "ManufactureTraceId", "ProductName", "ProductQuantityInStock" },
                values: new object[] { 1, 1, "SLS", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufactureTraceId",
                table: "Products",
                column: "ManufactureTraceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Manufactures");
        }
    }
}
