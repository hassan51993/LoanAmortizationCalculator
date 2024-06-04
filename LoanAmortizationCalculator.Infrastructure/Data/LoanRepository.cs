using LoanAmortizationCalculator.Domain.Entities;
using LoanAmortizationCalculator.Domain.Repositories;
using LoanAmortizationCalculator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanAmortizationCalculator.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LoanAmortizationContext _context;

        public LoanRepository(LoanAmortizationContext context)
        {
            _context = context;
        }

        public async Task<Loan> GetLoanByIdAsync(int id)
        {
            return await _context.Loans
                .Include(l => l.Payments)
                .Include(l => l.PaymentFrequency)
                .Include(l => l.LoanDurationUnit)
                .Include(l => l.GracePeriodUnit)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Loan>> GetAllLoansAsync()
        {
            return await _context.Loans
                .Include(l => l.Payments)
                .Include(l => l.PaymentFrequency)
                .Include(l => l.LoanDurationUnit)
                .Include(l => l.GracePeriodUnit)
                .ToListAsync();
        }

        public async Task AddLoanAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
        }

        public async Task UpdateLoanAsync(Loan loan) // Implement this method
        {
            _context.Loans.Update(loan);
        }

        public async Task DeleteLoanAsync(int id)
        {
            var loan = await GetLoanByIdAsync(id);
            if (loan != null)
            {
                _context.Loans.Remove(loan);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
