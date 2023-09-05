using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JackStockApi.Migrations
{
    /// <inheritdoc />
    public partial class StockMarketTypeCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "StockMarketType",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "StockMarketType");
        }
    }
}
