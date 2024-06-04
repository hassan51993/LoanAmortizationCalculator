using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanAmortizationCalculator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addBalanceBeforeAndBalanceAfterLoanCost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalCost",
                table: "Loans",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalInterest",
                table: "Loans",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BeginningBalance",
                table: "LoanPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EndingBalance",
                table: "LoanPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "TotalInterest",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "BeginningBalance",
                table: "LoanPayments");

            migrationBuilder.DropColumn(
                name: "EndingBalance",
                table: "LoanPayments");
        }
    }
}
