using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Result = table.Column<string>(type: "TEXT", nullable: true),
                    Ticker = table.Column<string>(type: "TEXT", nullable: true),
                    Entry = table.Column<double>(type: "REAL", nullable: false),
                    TakeProfit = table.Column<double>(type: "REAL", nullable: false),
                    StopLoss = table.Column<double>(type: "REAL", nullable: false),
                    RiskReward = table.Column<string>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Trades",
                columns: new[] { "Id", "Entry", "Notes", "Result", "RiskReward", "StopLoss", "TakeProfit", "Ticker", "Type" },
                values: new object[] { 1, 1.0259, "Test Notes", "WIN", "1:3", 1.0145, 1.05, "EUR/USD", 0 });

            migrationBuilder.InsertData(
                table: "Trades",
                columns: new[] { "Id", "Entry", "Notes", "Result", "RiskReward", "StopLoss", "TakeProfit", "Ticker", "Type" },
                values: new object[] { 2, 0.71213000000000004, "Test Notes", "LOSS", "1:5", 0.68000000000000005, 0.72999999999999998, "AUD/USD", 0 });

            migrationBuilder.InsertData(
                table: "Trades",
                columns: new[] { "Id", "Entry", "Notes", "Result", "RiskReward", "StopLoss", "TakeProfit", "Ticker", "Type" },
                values: new object[] { 3, 1.27728, "Test Notes", "WIN", "1:3", 1.2549999999999999, 1.3200000000000001, "USD/CAD", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trades");
        }
    }
}
