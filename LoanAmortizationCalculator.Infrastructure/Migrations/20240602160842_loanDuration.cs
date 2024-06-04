using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoanAmortizationCalculator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class loanDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_LoanDurations_LoanDurationId",
                table: "Loans");

            migrationBuilder.DropTable(
                name: "LoanDurations");

            migrationBuilder.DropIndex(
                name: "IX_Loans_LoanDurationId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "LoanDurationId",
                table: "Loans",
                newName: "LoanDurationValue");

            migrationBuilder.RenameColumn(
                name: "DurationValue",
                table: "Loans",
                newName: "LoanDurationUnitId");

            migrationBuilder.CreateTable(
                name: "LoanDurationUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDurationUnits", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "LoanDurationUnits",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Days" },
                    { 2, "Weeks" },
                    { 3, "Months" },
                    { 4, "Years" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_LoanDurationUnitId",
                table: "Loans",
                column: "LoanDurationUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_LoanDurationUnits_LoanDurationUnitId",
                table: "Loans",
                column: "LoanDurationUnitId",
                principalTable: "LoanDurationUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_LoanDurationUnits_LoanDurationUnitId",
                table: "Loans");

            migrationBuilder.DropTable(
                name: "LoanDurationUnits");

            migrationBuilder.DropIndex(
                name: "IX_Loans_LoanDurationUnitId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "LoanDurationValue",
                table: "Loans",
                newName: "LoanDurationId");

            migrationBuilder.RenameColumn(
                name: "LoanDurationUnitId",
                table: "Loans",
                newName: "DurationValue");

            migrationBuilder.CreateTable(
                name: "LoanDurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DurationUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDurations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "LoanDurations",
                columns: new[] { "Id", "DurationUnit", "Name" },
                values: new object[,]
                {
                    { 1, "Days", "Days" },
                    { 2, "Weeks", "Weeks" },
                    { 3, "Months", "Months" },
                    { 4, "Years", "Years" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_LoanDurationId",
                table: "Loans",
                column: "LoanDurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_LoanDurations_LoanDurationId",
                table: "Loans",
                column: "LoanDurationId",
                principalTable: "LoanDurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
