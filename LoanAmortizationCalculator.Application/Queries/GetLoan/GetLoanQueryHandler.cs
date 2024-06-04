using MediatR;
using LoanAmortizationCalculator.Domain.Repositories;
using LoanAmortizationCalculator.Application.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace LoanAmortizationCalculator.Application.Queries.GetLoan
{
    public class GetLoanQueryHandler : IRequestHandler<GetLoanQuery, LoanDto>
    {
        private readonly ILoanRepository _loanRepository;

        public GetLoanQueryHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<LoanDto> Handle(GetLoanQuery request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(request.LoanId);

            if (loan == null)
            {
                return null;
            }

            var loanDto = new LoanDto
            {
                Id = loan.Id,
                Amount = loan.Amount,
                DownPayment = loan.DownPayment,
                InterestRate = loan.InterestRate,
                StartDate = loan.StartDate,
                LoanDurationValue = loan.LoanDurationValue,
                LoanDurationUnit = loan.LoanDurationUnit.Name,
                PaymentFrequency = loan.PaymentFrequency.Name,
                TotalCost = loan.TotalCost,
                TotalInterest = loan.TotalInterest,
                Payments = loan.Payments.Select(p => new LoanPaymentDto
                {
                    PaymentNumber = p.PaymentNumber,
                    PaymentDate = p.PaymentDate,
                    Amount = p.Amount,
                    Principal = p.Principal,
                    Interest = p.Interest,
                    BeginningBalance = p.BeginningBalance,
                    EndingBalance = p.EndingBalance,
                    GraceEndDate = p.GraceEndDate
                }).ToList()
            };

            return loanDto;
        }
    }
}