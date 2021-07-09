using E_Loan.DataLayer;
using E_Loan.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services.Repository
{
    public class LoanManagerRepository : ILoanManagerRepository
    {
        /// <summary>
        /// Creating and injecting DbContext in LoanManagerRepository constructor
        /// </summary>
        private readonly UserMasterDbContext _loanContext;
        public LoanManagerRepository(UserMasterDbContext userMasterDbContext)
        {
            _loanContext = userMasterDbContext;
        }
        /// <summary>
        /// Accept loan application before start the loan approval process.
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public async Task<LoanMaster> AcceptLoanApplication(int loanId, string remark)
        {
            try
            {
                var findLoan = await _loanContext.loanMasters.FirstOrDefaultAsync(m => m.LoanId == loanId);
                if (findLoan.Status == LoanStatus.Received)
                {
                    findLoan.Status = LoanStatus.Accept;
                    findLoan.ManagerRemark = remark;
                    await _loanContext.SaveChangesAsync();
                }
                return findLoan;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Get list of all loan Application baed on status that is belongs to "Recived"
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LoanMaster>> AllLoanApplication()
        {
            try
            {
                var result = await _loanContext.loanMasters.
                Where(x => x.Status == LoanStatus.Received).Take(10).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Reject loan application after physical review with remark, before start the loan approval process make again as "Accept".
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public async Task<LoanMaster> RejectLoanApplication(int loanId, string remark)
        {
            try
            {
                var findLoan = await _loanContext.loanMasters.FirstOrDefaultAsync(m => m.LoanId == loanId);
                if (findLoan.Status == LoanStatus.Received)
                {
                    findLoan.Status = LoanStatus.Rejected;
                    findLoan.ManagerRemark = remark;
                    await _loanContext.SaveChangesAsync();
                }
                return findLoan;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Start the loan Sanction if loan status is "Accept" and add the all pending amout and all terms
        /// </summary>
        /// <param name="loanApprovaltrans"></param>
        /// <returns></returns>
        public async Task<LoanApprovaltrans> SanctionedLoan(LoanApprovaltrans loanApprovaltrans)
        {
            try
            {
                await _loanContext.loanApprovaltrans.AddAsync(loanApprovaltrans);
                await _loanContext.SaveChangesAsync();
                return loanApprovaltrans;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Using this methos check the loan status is "Accepted" or not before start loan process.
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public async Task<LoanMaster> CheckLoanStatus(int loanId)
        {
            try
            {
                var findLoan = await _loanContext.loanMasters.FirstOrDefaultAsync(m => m.LoanId == loanId);
                if (findLoan.Status == LoanStatus.Accept)
                {
                    return findLoan;
                }
                return findLoan;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
