using E_Loan.BusinessLayer;
using E_Loan.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services.Repository
{
    public interface ILoanAdminRepository
    {
        Task<IdentityRole> Login(LoginViewModel model);
    }
}
