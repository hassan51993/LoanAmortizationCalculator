using System;
using System.Text.Json.Serialization;

namespace LoanAmortizationCalculator.Domain.Entities
{
    public class LoanPayment
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal BeginningBalance { get; set; }
        public decimal EndingBalance { get; set; }
        public int PaymentNumber { get; set; }
        public int LoanId { get; set; }
        [JsonIgnore]
        public Loan Loan { get; set; }
        public DateTime GraceEndDate { get; set; } // تاريخ انتهاء فترة السماح لكل دفعة
    }
}
