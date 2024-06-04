using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanAmortizationCalculator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DownPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DownPayment",
                table: "Loans",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownPayment",
                table: "Loans");
        }
    }
}
