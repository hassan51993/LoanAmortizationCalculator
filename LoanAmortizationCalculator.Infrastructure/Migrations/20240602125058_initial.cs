using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoanAmortizationCalculator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanDurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DurationValue = table.Column<int>(type: "int", nullable: false),
                    DurationUnit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentFrequencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentFrequencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestRate = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoanDurationId = table.Column<int>(type: "int", nullable: false),
                    PaymentFrequencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_LoanDurations_LoanDurationId",
                        column: x => x.LoanDurationId,
                        principalTable: "LoanDurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_PaymentFrequencies_PaymentFrequencyId",
                        column: x => x.PaymentFrequencyId,
                        principalTable: "PaymentFrequencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Principal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentNumber = table.Column<int>(type: "int", nullable: false),
                    LoanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanPayments_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LoanDurations",
                columns: new[] { "Id", "DurationUnit", "DurationValue" },
                values: new object[,]
                {
                    { 1, "Days", 1 },
                    { 2, "Days", 30 },
                    { 3, "Months", 6 },
                    { 4, "Years", 1 }
                });

            migrationBuilder.InsertData(
                table: "PaymentFrequencies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Daily" },
                    { 2, "Monthly" },
                    { 3, "Quarterly" },
                    { 4, "SemiAnnually" },
                    { 5, "Annually" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanPayments_LoanId",
                table: "LoanPayments",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_LoanDurationId",
                table: "Loans",
                column: "LoanDurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_PaymentFrequencyId",
                table: "Loans",
                column: "PaymentFrequencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanPayments");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "LoanDurations");

            migrationBuilder.DropTable(
                name: "PaymentFrequencies");
        }
    }
}
