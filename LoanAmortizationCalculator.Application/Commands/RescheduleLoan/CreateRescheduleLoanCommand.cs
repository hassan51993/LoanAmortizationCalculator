using MediatR;

namespace LoanAmortizationCalculator.Application.Commands.RescheduleLoan
{
    public class CreateRescheduleLoanCommand : IRequest<int>
    {
        public int LoanId { get; set; }
        public double NewInterestRate { get; set; }
    }
}
