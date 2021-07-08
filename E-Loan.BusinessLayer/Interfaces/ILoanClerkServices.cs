using E_Loan.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Interfaces
{
    public interface ILoanClerkServices
    {
        Task<IEnumerable<LoanMaster>> AllLoanApplication();
        Task<IEnumerable<LoanMaster>> RecivedLoanApplication();
        Task<IEnumerable<LoanMaster>> NotRecivedLoanApplication();
        Task<LoanProcesstrans> ProcessLoan(LoanProcesstrans loanProcesstrans);
        Task<LoanMaster> RecivedLoan(int loanId);
    }
}
