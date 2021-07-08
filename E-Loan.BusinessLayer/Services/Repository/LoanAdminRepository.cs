using E_Loan.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services.Repository
{
    public class LoanAdminRepository : ILoanAdminRepository
    {
        private readonly UserManager<UserMaster> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        public LoanAdminRepository() { }
        public LoanAdminRepository(UserManager<UserMaster> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        public Task<IdentityRole> Login(LoginViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
