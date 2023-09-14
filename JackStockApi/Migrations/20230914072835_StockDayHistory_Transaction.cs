using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JackStockApi.Migrations
{
    /// <inheritdoc />
    public partial class StockDayHistory_Transaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Transaction",
                table: "StockDayHistory",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Transaction",
                table: "StockDayHistory",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
