﻿using E_Loan.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services.Repository
{
    public interface ILoanClerkRepository
    {
        Task<IEnumerable<LoanMaster>> AllLoanApplication();
        Task<IEnumerable<LoanMaster>> RecivedLoanApplication();
        Task<IEnumerable<LoanMaster>> NotRecivedLoanApplication();
        Task<LoanProcesstrans> ProcessLoan(LoanProcesstrans loanProcesstrans);
        Task<LoanMaster> RecivedLoan(int loanId);
    }
}
