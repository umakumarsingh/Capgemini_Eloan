using E_Loan.Entities;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services.Repository
{
    public interface ILoanCustomerRepository
    {
        Task<LoanMaster> ApplyMortgage(LoanMaster loanMaster);
        Task<LoanMaster> UpdateMortgage(LoanMaster loanMaster);
        Task<LoanMaster> AppliedLoanStatus(int loanId);
    }
}
