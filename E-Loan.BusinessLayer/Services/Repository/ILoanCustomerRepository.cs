using E_Loan.Entities;
using System;
using System.Collections.Generic;
using System.Text;
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
