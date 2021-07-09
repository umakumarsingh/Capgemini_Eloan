using E_Loan.BusinessLayer.Interfaces;
using E_Loan.BusinessLayer.Services.Repository;
using E_Loan.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services
{
    public class LoanAdminServices : ILoanAdminServices
    {
        /// <summary>
        /// Creating instance/field of ILoanCustomerRepository and injecting into LoanCustomerSevices Constructor
        /// </summary>
        private readonly ILoanAdminRepository _adminRepository;
        public LoanAdminServices(ILoanAdminRepository loanAdminRepository)
        {
            _adminRepository = loanAdminRepository;
        }
        public async Task<IdentityResult> CreateRole(CreateRoleViewModel model)
        {
            return await _adminRepository.CreateRole(model);
        }

        public async Task<IdentityResult> DisableUser(string userId)
        {
            return await _adminRepository.DisableUser(userId);   
        }

        public async Task<IdentityResult> EditRole(EditRoleViewModel model)
        {
            return await _adminRepository.EditRole(model);
        }

        public async Task<IdentityResult> ChangeUserPassword(ChangePasswordViewModel model)
        {
            return await _adminRepository.ChangeUserPassword(model);
        }

        public async Task<IdentityResult> EditUsersInRole(UserRoleViewModel model, string roleId)
        {
            return await _adminRepository.EditUsersInRole(model, roleId);
        }

        public async Task<IdentityResult> EnableUser(string userId)
        {
            return await _adminRepository.EnableUser(userId);
        }

        public async Task<UserMaster> FindByEmailAsync(string email)
        {
            return await _adminRepository.FindByEmailAsync(email);
        }

        public async Task<IdentityRole> FindRoleByRoleId(string roleId)
        {
            return await _adminRepository.FindRoleByRoleId(roleId);
        }

        public async Task<IdentityRole> FindRoleByRoleName(string roleName)
        {
            return await _adminRepository.FindRoleByRoleName(roleName);
        }

        public async Task<IEnumerable<IdentityRole>> ListAllRole()
        {
            return await _adminRepository.ListAllRole();
        }

        public async Task<IEnumerable<UserMaster>> ListAllUser()
        {
            return await _adminRepository.ListAllUser();
        }

        public Task<string> Login(LoginViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<UserMaster> FindUserByIdAsync(string userId)
        {
            return await _adminRepository.FindUserByIdAsync(userId);
        }
    }
}
