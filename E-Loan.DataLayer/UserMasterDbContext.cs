using E_Loan.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Loan.DataLayer
{
    public class UserMasterDbContext : IdentityDbContext<UserMaster>
    {
        public UserMasterDbContext(DbContextOptions<UserMasterDbContext> options) : base(options)
        {

        }
        /// <summary>
        /// Seed and create DbSet for all loan class
        /// </summary>
        public DbSet<LoanMaster> loanMasters { get; set; }
        public DbSet<LoanProcesstrans> loanProcesstrans { get; set; }
        public DbSet<LoanApprovaltrans> loanApprovaltrans { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
        builder.Entity<LoanMaster>().HasKey(x => x.LoanId);
            builder.Entity<LoanProcesstrans>().HasKey(x => x.Id);
            builder.Entity<LoanApprovaltrans>().HasKey(x => x.Id);
            base.OnModelCreating(builder);
        }
    }
}
