﻿using E_Loan.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services.Repository
{
    public interface ILoanManagerRepository
    {
        Task<IEnumerable<LoanMaster>> AllLoanApplication();
        Task<LoanMaster> AcceptLoanApplication(int loanId, string remark);
        Task<LoanMaster> RejectLoanApplication(int loanId, string remark);
        Task<LoanApprovaltrans> SanctionedLoan(LoanApprovaltrans loanApprovaltrans);
        Task<LoanMaster> CheckLoanStatus(int loanId);
    }
}
