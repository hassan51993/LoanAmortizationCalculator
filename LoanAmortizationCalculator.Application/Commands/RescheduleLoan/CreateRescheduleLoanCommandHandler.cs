using MediatR;
using LoanAmortizationCalculator.Domain.Entities;
using LoanAmortizationCalculator.Domain.Repositories;
using LoanAmortizationCalculator.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace LoanAmortizationCalculator.Application.Commands.RescheduleLoan
{
    public class CreateRescheduleLoanCommandHandler : IRequestHandler<CreateRescheduleLoanCommand, int>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly LoanService _loanService;

        public CreateRescheduleLoanCommandHandler(ILoanRepository loanRepository, LoanService loanService)
        {
            _loanRepository = loanRepository;
            _loanService = loanService;
        }

        public async Task<int> Handle(CreateRescheduleLoanCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the existing loan
            var loan = await _loanRepository.GetLoanByIdAsync(request.LoanId);
            if (loan == null)
            {
                throw new ArgumentException("Loan not found");
            }

            // Update the interest rate
            loan.InterestRate = request.NewInterestRate;

            // Recalculate the amortization schedule
            var (payments, totalCost, totalInterest) = _loanService.CalculateAmortizationSchedule(loan);
            loan.Payments = payments;
            loan.TotalCost = totalCost;
            loan.TotalInterest = totalInterest;

            // Update the loan in the repository
            await _loanRepository.UpdateLoanAsync(loan);
            await _loanRepository.SaveChangesAsync();

            return loan.Id;
        }
    }
}
