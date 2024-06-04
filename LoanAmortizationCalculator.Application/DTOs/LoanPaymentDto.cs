using System;

namespace LoanAmortizationCalculator.Application.DTOs
{
    public class LoanPaymentDto
    {
        public int PaymentNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal BeginningBalance { get; set; }
        public decimal EndingBalance { get; set; }
        public DateTime GraceEndDate { get; set; }
    }
}