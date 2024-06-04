using System;
using System.Collections.Generic;

namespace LoanAmortizationCalculator.Application.DTOs
{
    public class LoanDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal DownPayment { get; set; }
        public double InterestRate { get; set; }
        public DateTime StartDate { get; set; }
        public int LoanDurationValue { get; set; }
        public string LoanDurationUnit { get; set; }
        public string PaymentFrequency { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalInterest { get; set; }
        public List<LoanPaymentDto> Payments { get; set; }
    }
}