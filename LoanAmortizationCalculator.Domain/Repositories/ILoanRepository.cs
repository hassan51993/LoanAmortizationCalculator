using LoanAmortizationCalculator.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanAmortizationCalculator.Domain.Repositories
{
    public interface ILoanRepository
    {
        Task<Loan> GetLoanByIdAsync(int id);
        Task<IEnumerable<Loan>> GetAllLoansAsync();
        Task AddLoanAsync(Loan loan);
        Task UpdateLoanAsync(Loan loan); // Add this method
        Task DeleteLoanAsync(int id);
        Task SaveChangesAsync();
    }
}
