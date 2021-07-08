using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services.Repository
{
    public interface ILoanAdminRepository
    {
        Task<IdentityRole> Login(LoginViewModel model);
    }
}
