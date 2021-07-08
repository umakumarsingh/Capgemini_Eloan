using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Interfaces
{
    public interface ILoanAdminServices
    {
        Task<IdentityRole> Login(LoginViewModel model);
    }
}
