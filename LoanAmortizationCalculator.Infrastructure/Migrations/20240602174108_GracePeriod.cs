using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoanAmortizationCalculator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GracePeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GracePeriodUnitId",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GracePeriodValue",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "GraceEndDate",
                table: "LoanPayments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "GracePeriodUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GracePeriodUnits", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "GracePeriodUnits",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Days" },
                    { 2, "Weeks" },
                    { 3, "Months" },
                    { 4, "Years" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_GracePeriodUnitId",
                table: "Loans",
                column: "GracePeriodUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_GracePeriodUnits_GracePeriodUnitId",
                table: "Loans",
                column: "GracePeriodUnitId",
                principalTable: "GracePeriodUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_GracePeriodUnits_GracePeriodUnitId",
                table: "Loans");

            migrationBuilder.DropTable(
                name: "GracePeriodUnits");

            migrationBuilder.DropIndex(
                name: "IX_Loans_GracePeriodUnitId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "GracePeriodUnitId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "GracePeriodValue",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "GraceEndDate",
                table: "LoanPayments");
        }
    }
}
