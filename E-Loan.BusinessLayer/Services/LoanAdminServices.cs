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
        /// <summary>
        /// Create a new role if role is exists not possible to create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateRole(CreateRoleViewModel model)
        {
            return await _adminRepository.CreateRole(model);
        }
        /// <summary>
        /// Disable an existing use if not required to work and login provide userId as GUID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IdentityResult> DisableUser(string userId)
        {
            return await _adminRepository.DisableUser(userId);   
        }
        /// <summary>
        /// Edit an existing role if required using below method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> EditRole(EditRoleViewModel model)
        {
            return await _adminRepository.EditRole(model);
        }
        /// <summary>
        /// Edit register user or change user password only, UserName/Name and Email are not change and must provide
        /// Change user Password only - Applicable using below method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> ChangeUserPassword(ChangePasswordViewModel model)
        {
            return await _adminRepository.ChangeUserPassword(model);
        }
        /// <summary>
        /// Provide different role for registered User
        /// </summary>
        /// <param name="model"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IdentityResult> EditUsersInRole(UserRoleViewModel model, string roleId)
        {
            return await _adminRepository.EditUsersInRole(model, roleId);
        }
        /// <summary>
        /// Enable an existing user user id must be supplied GUID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IdentityResult> EnableUser(string userId)
        {
            return await _adminRepository.EnableUser(userId);
        }
        /// <summary>
        /// Find user by user emailId
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserMaster> FindByEmailAsync(string email)
        {
            return await _adminRepository.FindByEmailAsync(email);
        }
        /// <summary>
        /// Find an existing role by role id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IdentityRole> FindRoleByRoleId(string roleId)
        {
            return await _adminRepository.FindRoleByRoleId(roleId);
        }
        /// <summary>
        /// Find an existing role by role name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<IdentityRole> FindRoleByRoleName(string roleName)
        {
            return await _adminRepository.FindRoleByRoleName(roleName);
        }
        /// <summary>
        /// List all user role from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<IdentityRole>> ListAllRole()
        {
            return await _adminRepository.ListAllRole();
        }
        /// <summary>
        /// List all user from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserMaster>> ListAllUser()
        {
            return await _adminRepository.ListAllUser();
        }
        /// <summary>
        /// Find an existing user bu userId Loginusing this method and return JWT token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<string> Login(LoginViewModel model)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Find user by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserMaster> FindUserByIdAsync(string userId)
        {
            return await _adminRepository.FindUserByIdAsync(userId);
        }
        /// <summary>
        /// Register new user with default user role is customer
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<IdentityResult> Register(UserMaster user, string password)
        {
            return await _adminRepository.Register(user, password);
        }
    }
}
