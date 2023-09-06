using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JackStockApi.Migrations
{
    /// <inheritdoc />
    public partial class StockDayHistory2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockHistory");

            migrationBuilder.CreateTable(
                name: "StockDayHistory",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "date", nullable: false),
                    TradeVolumn = table.Column<long>(type: "bigint", nullable: false),
                    TradeValue = table.Column<long>(type: "bigint", nullable: false),
                    OpeningPrice = table.Column<double>(type: "float", nullable: false),
                    HighestPrice = table.Column<double>(type: "float", nullable: false),
                    LowestPrice = table.Column<double>(type: "float", nullable: false),
                    ClosingPrice = table.Column<double>(type: "float", nullable: false),
                    Change = table.Column<double>(type: "float", nullable: false),
                    Transaction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockDayHistory", x => new { x.StockId, x.DateTime });
                    table.ForeignKey(
                        name: "FK_StockDayHistory_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockDayHistory");

            migrationBuilder.CreateTable(
                name: "StockHistory",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "date", nullable: false),
                    Change = table.Column<double>(type: "float", nullable: false),
                    ClosingPrice = table.Column<double>(type: "float", nullable: false),
                    HighestPrice = table.Column<double>(type: "float", nullable: false),
                    LowestPrice = table.Column<double>(type: "float", nullable: false),
                    OpeningPrice = table.Column<double>(type: "float", nullable: false),
                    TradeValue = table.Column<long>(type: "bigint", nullable: false),
                    TradeVolumn = table.Column<long>(type: "bigint", nullable: false),
                    Transaction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockHistory", x => new { x.StockId, x.DateTime });
                    table.ForeignKey(
                        name: "FK_StockHistory_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
