using MediatR;

namespace LoanAmortizationCalculator.Application.Commands.CreateLoan
{
    public class CreateLoanCommand : IRequest<int>
    {
        public decimal Amount { get; set; }
        public decimal DownPayment { get; set; } // المبلغ المدفوع مقدماً
        public double InterestRate { get; set; }
        public DateTime StartDate { get; set; }
        public int LoanDurationValue { get; set; }
        public int LoanDurationUnitId { get; set; }
        public int PaymentFrequencyId { get; set; }
        public int GracePeriodValue { get; set; } = 0; // القيمة الافتراضية لفترة السماح صفر أيام
        public int GracePeriodUnitId { get; set; } // تأكد من تعيين الوحدة الافتراضية إذا لزم الأمر
    }
}
