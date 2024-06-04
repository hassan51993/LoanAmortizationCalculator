using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LoanAmortizationCalculator.Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public decimal Amount { get; set; } // المبلغ الإجمالي للقرض
        public decimal DownPayment { get; set; } // المبلغ المدفوع مقدماً
        public double InterestRate { get; set; }
        public DateTime StartDate { get; set; }
        public int LoanDurationValue { get; set; }
        public int LoanDurationUnitId { get; set; }
        public LoanDurationUnit LoanDurationUnit { get; set; }
        public int PaymentFrequencyId { get; set; }
        public PaymentFrequency PaymentFrequency { get; set; }
        [JsonIgnore]
        public List<LoanPayment> Payments { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalInterest { get; set; }
        public int GracePeriodValue { get; set; }
        public int GracePeriodUnitId { get; set; }
        public GracePeriodUnit GracePeriodUnit { get; set; }
    }
}
