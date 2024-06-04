using MediatR;
using LoanAmortizationCalculator.Application.DTOs;

namespace LoanAmortizationCalculator.Application.Queries.GetLoan
{
    public class GetLoanQuery : IRequest<LoanDto>
    {
        public int LoanId { get; set; }
    }
}