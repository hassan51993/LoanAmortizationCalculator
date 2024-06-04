namespace LoanAmortizationCalculator.API.DTOs
{
    public class RescheduleLoanRequest
    {
        public int LoanId { get; set; }
        public double NewInterestRate { get; set; }
    }
}
