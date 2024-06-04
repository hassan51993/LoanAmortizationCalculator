using LoanAmortizationCalculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoanAmortizationCalculator.Infrastructure.Data
{
    public class LoanAmortizationContext : DbContext
    {
        public LoanAmortizationContext(DbContextOptions<LoanAmortizationContext> options)
        : base(options)
        {
        }

        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanPayment> LoanPayments { get; set; }
        public DbSet<PaymentFrequency> PaymentFrequencies { get; set; }
        public DbSet<LoanDurationUnit> LoanDurationUnits { get; set; }
        public DbSet<GracePeriodUnit> GracePeriodUnits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>()
            .HasMany(l => l.Payments)
            .WithOne(p => p.Loan)
            .HasForeignKey(p => p.LoanId);

            modelBuilder.Entity<LoanPayment>()
             .Property(p => p.PaymentNumber)
             .IsRequired();

            modelBuilder.Entity<Loan>()
             .HasOne(l => l.PaymentFrequency)
             .WithMany()
             .HasForeignKey(l => l.PaymentFrequencyId)
             .IsRequired();

            modelBuilder.Entity<Loan>()
             .HasOne(l => l.LoanDurationUnit)
             .WithMany()
             .HasForeignKey(l => l.LoanDurationUnitId)
             .IsRequired();

            modelBuilder.Entity<Loan>()
             .HasOne(l => l.GracePeriodUnit)
             .WithMany()
             .HasForeignKey(l => l.GracePeriodUnitId)
            .IsRequired();

            modelBuilder.Entity<PaymentFrequency>()
             .HasData(
             new PaymentFrequency { Id = 1, Name = "Daily" },
             new PaymentFrequency { Id = 2, Name = "Weekly" },
             new PaymentFrequency { Id = 3, Name = "Monthly" },
             new PaymentFrequency { Id = 4, Name = "Quarterly" },
             new PaymentFrequency { Id = 5, Name = "SemiAnnually" },
             new PaymentFrequency { Id = 6, Name = "Annually" }
             );

            modelBuilder.Entity<LoanDurationUnit>()
             .HasData(
             new LoanDurationUnit { Id = 1, Name = "Days" },
             new LoanDurationUnit { Id = 2, Name = "Weeks" },
             new LoanDurationUnit { Id = 3, Name = "Months" },
             new LoanDurationUnit { Id = 4, Name = "Years" }
             );

            modelBuilder.Entity<GracePeriodUnit>()
             .HasData(
             new GracePeriodUnit { Id = 1, Name = "Days" },
             new GracePeriodUnit { Id = 2, Name = "Weeks" },
             new GracePeriodUnit { Id = 3, Name = "Months" },
             new GracePeriodUnit { Id = 4, Name = "Years" }
             );
        }
    }
}