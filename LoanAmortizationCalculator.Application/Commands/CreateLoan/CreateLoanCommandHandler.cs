using MediatR;
using LoanAmortizationCalculator.Domain.Entities;
using LoanAmortizationCalculator.Domain.Repositories;
using LoanAmortizationCalculator.Application.Services;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LoanAmortizationCalculator.Application.Commands.CreateLoan
{
    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, int>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly LoanService _loanService;

        public CreateLoanCommandHandler(ILoanRepository loanRepository, LoanService loanService)
        {
            _loanRepository = loanRepository;
            _loanService = loanService;
        }

        public async Task<int> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = new Loan
            {
                Amount = request.Amount,
                DownPayment = request.DownPayment, // تضمين المبلغ المدفوع مقدماً
                InterestRate = request.InterestRate,
                StartDate = request.StartDate,
                LoanDurationValue = request.LoanDurationValue,
                LoanDurationUnitId = request.LoanDurationUnitId,
                PaymentFrequencyId = request.PaymentFrequencyId,
                GracePeriodValue = request.GracePeriodValue != 0 ? request.GracePeriodValue : 0, // تعيين القيمة الافتراضية إذا لم يتم تقديمها
                GracePeriodUnitId = request.GracePeriodUnitId,
                Payments = new List<LoanPayment>()
            };

            var (payments, totalCost, totalInterest) = _loanService.CalculateAmortizationSchedule(loan);
            loan.Payments = payments;
            loan.TotalCost = totalCost;
            loan.TotalInterest = totalInterest;

            await _loanRepository.AddLoanAsync(loan);
            await _loanRepository.SaveChangesAsync();

            return loan.Id;
        }
    }
}
