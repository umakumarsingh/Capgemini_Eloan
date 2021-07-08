using E_Loan.DataLayer;
using E_Loan.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services.Repository
{
    public class LoanCustomerRepository : ILoanCustomerRepository
    {
        /// <summary>
        /// Creating and injecting DbContext in LoanCustomerRepository constructor
        /// </summary>
        private readonly UserMasterDbContext _loanContext;
        public LoanCustomerRepository(UserMasterDbContext userMasterDbContext)
        {
            _loanContext = userMasterDbContext;
        }
        /// <summary>
        /// Apply mortage and save all data in sql server.
        /// </summary>
        /// <param name="loanMaster"></param>
        /// <returns></returns>
        public async Task<LoanMaster> ApplyMortgage(LoanMaster loanMaster)
        {
            try
            {
                await _loanContext.loanMasters.AddAsync(loanMaster);
                await _loanContext.SaveChangesAsync();
                return loanMaster;
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Get the loan status by id
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public async Task<LoanMaster> AppliedLoanStatus(int loanId)
        {
            try
            {
                return await _loanContext.loanMasters.FirstOrDefaultAsync(m => m.LoanId == loanId);
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Update the existing loan application before sent to loan clerk
        /// </summary>
        /// <param name="loanMaster"></param>
        /// <returns></returns>
        public async Task<LoanMaster> UpdateMortgage(LoanMaster loanMaster)
        {

            try
            {
                _loanContext.loanMasters.Update(loanMaster);
                await _loanContext.SaveChangesAsync();
                return loanMaster;
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
    }
}
