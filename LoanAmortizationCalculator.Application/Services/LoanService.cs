using LoanAmortizationCalculator.Domain.Entities;
using LoanAmortizationCalculator.Infrastructure.Data;
using System;
using System.Collections.Generic;

namespace LoanAmortizationCalculator.Application.Services
{
    public class LoanService
    {
        private readonly LoanAmortizationContext _context;

        public LoanService(LoanAmortizationContext context)
        {
            _context = context;
        }

        public (List<LoanPayment>, decimal totalCost, decimal totalInterest) CalculateAmortizationSchedule(Loan loan)
        {
            var payments = new List<LoanPayment>();

            var loanDurationUnit = _context.LoanDurationUnits.Find(loan.LoanDurationUnitId);
            var gracePeriodUnit = _context.GracePeriodUnits.Find(loan.GracePeriodUnitId);
            if (loanDurationUnit == null)
            {
                throw new ArgumentException("Invalid LoanDurationUnitId");
            }
            if (gracePeriodUnit == null)
            {
                throw new ArgumentException("Invalid GracePeriodUnitId");
            }

            int numberOfDays = GetNumberOfDays(loan.LoanDurationValue, loanDurationUnit.Name);
            int numberOfPayments = numberOfDays / GetDaysPerPayment(loan.PaymentFrequencyId);

            decimal interestRatePerPeriod = (decimal)loan.InterestRate / (365 / GetDaysPerPayment(loan.PaymentFrequencyId)) / 100;
            decimal financedAmount = loan.Amount - loan.DownPayment;
            decimal paymentAmount = financedAmount * interestRatePerPeriod / (1 - (decimal)Math.Pow(1 + (double)interestRatePerPeriod, -numberOfPayments));
            decimal remainingBalance = financedAmount;
            decimal totalCost = 0;
            decimal totalInterest = 0;

            int gracePeriodDays = GetNumberOfDays(loan.GracePeriodValue, gracePeriodUnit.Name);

            for (int i = 1; i <= numberOfPayments; i++)
            {
                decimal interest = remainingBalance * interestRatePerPeriod;
                decimal principal = paymentAmount - interest;
                decimal beginningBalance = remainingBalance;
                remainingBalance -= principal;
                decimal endingBalance = remainingBalance;

                totalCost += paymentAmount;
                totalInterest += interest;

                DateTime paymentDate = loan.StartDate.AddDays((i - 1) * GetDaysPerPayment(loan.PaymentFrequencyId));
                DateTime graceEndDate = paymentDate.AddDays(gracePeriodDays);

                payments.Add(new LoanPayment
                {
                    PaymentNumber = i,
                    PaymentDate = paymentDate,
                    GraceEndDate = graceEndDate,
                    Amount = paymentAmount,
                    Principal = principal,
                    Interest = interest,
                    BeginningBalance = beginningBalance,
                    EndingBalance = endingBalance,
                    LoanId = loan.Id
                });
            }

            return (payments, totalCost, totalInterest);
        }

        private int GetNumberOfDays(int value, string unit)
        {
            switch (unit)
            {
                case "Days":
                    return value;
                case "Weeks":
                    return value * 7;
                case "Months":
                    return value * 30;
                case "Years":
                    return value * 365;
                default:
                    throw new ArgumentException("Invalid unit");
            }
        }

        private int GetDaysPerPayment(int paymentFrequencyId)
        {
            switch (paymentFrequencyId)
            {
                case 1: // Daily
                    return 1;
                case 2: // Weekly
                    return 7;
                case 3: // Monthly
                    return 30;
                case 4: // Quarterly
                    return 90;
                case 5: // SemiAnnually
                    return 180;
                case 6: // Annually
                    return 365;
                default:
                    throw new ArgumentException("Invalid PaymentFrequencyId");
            }
        }
    }
}
