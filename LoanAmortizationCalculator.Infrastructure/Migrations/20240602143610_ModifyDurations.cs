using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanAmortizationCalculator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationValue",
                table: "LoanDurations");

            migrationBuilder.AddColumn<int>(
                name: "DurationValue",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LoanDurations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "LoanDurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Days");

            migrationBuilder.UpdateData(
                table: "LoanDurations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DurationUnit", "Name" },
                values: new object[] { "Weeks", "Weeks" });

            migrationBuilder.UpdateData(
                table: "LoanDurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Months");

            migrationBuilder.UpdateData(
                table: "LoanDurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Years");

            migrationBuilder.UpdateData(
                table: "PaymentFrequencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Weekly");

            migrationBuilder.UpdateData(
                table: "PaymentFrequencies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Monthly");

            migrationBuilder.UpdateData(
                table: "PaymentFrequencies",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Quarterly");

            migrationBuilder.UpdateData(
                table: "PaymentFrequencies",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "SemiAnnually");

            migrationBuilder.InsertData(
                table: "PaymentFrequencies",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "Annually" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentFrequencies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "DurationValue",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "LoanDurations");

            migrationBuilder.AddColumn<int>(
                name: "DurationValue",
                table: "LoanDurations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "LoanDurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "DurationValue",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LoanDurations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DurationUnit", "DurationValue" },
                values: new object[] { "Days", 30 });

            migrationBuilder.UpdateData(
                table: "LoanDurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "DurationValue",
                value: 6);

            migrationBuilder.UpdateData(
                table: "LoanDurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "DurationValue",
                value: 1);

            migrationBuilder.UpdateData(
                table: "PaymentFrequencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Monthly");

            migrationBuilder.UpdateData(
                table: "PaymentFrequencies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Quarterly");

            migrationBuilder.UpdateData(
                table: "PaymentFrequencies",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "SemiAnnually");

            migrationBuilder.UpdateData(
                table: "PaymentFrequencies",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Annually");
        }
    }
}
