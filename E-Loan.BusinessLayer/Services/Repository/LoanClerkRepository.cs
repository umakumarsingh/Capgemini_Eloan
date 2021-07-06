using E_Loan.DataLayer;
using E_Loan.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services.Repository
{
    public class LoanClerkRepository : ILoanClerkRepository
    {
        /// <summary>
        /// Creating and injecting DbContext in LoanClerkRepository constructor
        /// </summary>
        private readonly UserMasterDbContext _loanContext;
        public LoanClerkRepository(UserMasterDbContext userMasterDbContext)
        {
            _loanContext = userMasterDbContext;
        }
        /// <summary>
        /// Show/Get all loan application
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LoanMaster>> AllLoanApplication()
        {
            try
            {
                var result = await _loanContext.loanMasters.
                OrderByDescending(x => x.Date).Take(10).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Show/Get all loan application that status is Not Recived
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LoanMaster>> NotRecivedLoanApplication()
        {
            try
            {
                var result = await _loanContext.loanMasters.
                Where( x => x.Status == LoanStatus.NotRecived).Take(10).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Start the loan process and add the remening data by loan clerk
        /// </summary>
        /// <param name="loanProcesstrans"></param>
        /// <returns></returns>
        public async Task<LoanProcesstrans> ProcessLoan(LoanProcesstrans loanProcesstrans)
        {
            try
            {
                await _loanContext.loanProcesstrans.AddAsync(loanProcesstrans);
                await _loanContext.SaveChangesAsync();
                return loanProcesstrans;
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<LoanMaster> RecivedLoan(int loanId)
        {
            try
            {
                var findLoan = await _loanContext.loanMasters.FirstOrDefaultAsync(m => m.LoanId == loanId);
                if(findLoan.Status == LoanStatus.NotRecived)
                {
                    LoanMaster ediLoan = new LoanMaster
                    {
                        Status = LoanStatus.Recived
                    };
                    _loanContext.loanMasters.Update(ediLoan);
                    await _loanContext.SaveChangesAsync();
                }
                return findLoan;
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<IEnumerable<LoanMaster>> RecivedLoanApplication()
        {
            try
            {
                var result = await _loanContext.loanMasters.
                Where(x => x.Status == LoanStatus.Recived).Take(10).ToListAsync();
                return result;
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }

    }
}
