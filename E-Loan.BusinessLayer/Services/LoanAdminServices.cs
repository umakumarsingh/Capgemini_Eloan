using E_Loan.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services
{
    public class LoanAdminServices : ILoanAdminServices
    {
        public Task<IdentityRole> Login(LoginViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
