using E_Loan.BusinessLayer.Interfaces;
using E_Loan.BusinessLayer.Services.Repository;
using E_Loan.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services
{
    public class LoanClerkServices : ILoanClerkServices
    {
        /// <summary>
        /// Creating the ILoanClerkRepository field/instance and injecting in LoanClerkServices constuctor
        /// </summary>
        private readonly ILoanClerkRepository _clerkRepository;
        public LoanClerkServices(ILoanClerkRepository loanClerkRepository)
        {
            _clerkRepository = loanClerkRepository;
        }
        /// <summary>
        /// Get/Show all loan application
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LoanMaster>> AllLoanApplication()
        {
            return await _clerkRepository.AllLoanApplication();
        }
        /// <summary>
        /// Show not recived loan application
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LoanMaster>> NotRecivedLoanApplication()
        {
           return await _clerkRepository.NotRecivedLoanApplication();
        }
        /// <summary>
        /// Start the loan process after all status verifaction by loan clerk
        /// </summary>
        /// <param name="loanProcesstrans"></param>
        /// <returns></returns>
        public async Task<LoanProcesstrans> ProcessLoan(LoanProcesstrans loanProcesstrans)
        {
            var result = await _clerkRepository.ProcessLoan(loanProcesstrans);
            return result;
        }
        /// <summary>
        /// Check the status of recived loan application before start the loan process
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public async Task<LoanMaster> RecivedLoan(int loanId)
        {
            return await _clerkRepository.RecivedLoan(loanId);
        }
        /// <summary>
        /// Sho/get all loan application thta is in recived form
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LoanMaster>> RecivedLoanApplication()
        {
            return await _clerkRepository.RecivedLoanApplication();
        }
    }
}
