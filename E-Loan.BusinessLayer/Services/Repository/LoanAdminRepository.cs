using E_Loan.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Loan.BusinessLayer.Services.Repository
{
    public class LoanAdminRepository : ILoanAdminRepository
    {
        /// <summary>
        /// Creating and injecting UserManager, RoleManager in LoanAdminRepository constructor
        /// </summary>
        private readonly UserManager<UserMaster> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        public LoanAdminRepository(UserManager<UserMaster> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }
        /// <summary>
        /// Create a new role if role is exists not possible to create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateRole(CreateRoleViewModel model)
        {
            try
            {
                IdentityRole identityRole = new IdentityRole { Name = model.RoleName.Trim() };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                return result;
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Disable an existing use if not required to work and login provide userId as GUID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IdentityResult> DisableUser(string userId)
        {
            var user = await FindUserByIdAsync(userId);
            bool newStatus = true;
            try
            {
                user.Enabled = newStatus;
                return await userManager.UpdateAsync(user);
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Edit an existing role if required using below method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            try
            {
                role.Name = model.RoleName;
                return await roleManager.UpdateAsync(role);
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Edit register user or change user password only, UserName/Name and Email are not change and must provide
        /// Change user Password only - Applicable using below method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> ChangeUserPassword(ChangePasswordViewModel model)
        {
            var user = await FindByEmailAsync(model.Email);
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            try
            {
              return  await userManager.ResetPasswordAsync(user, token, model.Password);
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Provide different role for registered User
        /// </summary>
        /// <param name="model"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IdentityResult> EditUsersInRole(UserRoleViewModel model, string roleId)
        {
            IdentityResult result = null;
            var role = await FindRoleByRoleId(roleId);
            try
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (!await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                return result;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        /// <summary>
        /// Enable an existing user user id must be supplied GUID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IdentityResult> EnableUser(string userId)
        {
            var user = await FindUserByIdAsync(userId);
            bool newStatus = false;
            try
            {
                user.Enabled = newStatus;
                return await userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Find user by user emailId
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserMaster> FindByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }
        /// <summary>
        /// Find an existing role by role id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IdentityRole> FindRoleByRoleId(string roleId)
        {
            return await roleManager.FindByIdAsync(roleId);
        }
        /// <summary>
        /// Find an existing role by role name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<IdentityRole> FindRoleByRoleName(string roleName)
        {
            try
            {
                return await roleManager.FindByNameAsync(roleName);
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// List all user role from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<IdentityRole>> ListAllRole()
        {
            try
            {
                return await roleManager.Roles.ToListAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// List all user from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserMaster>> ListAllUser()
        {
            try
            {
                return await userManager.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Find an existing user bu userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserMaster> FindUserByIdAsync(string userId)
        {
            try
            {
                return await userManager.FindByIdAsync(userId);
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Login by id and password return JWT token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> Login(LoginViewModel model)
        {
            throw new NotImplementedException();
            //var user = await FindByEmailAsync(model.Email);
            //try
            //{
            //    if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            //    {
            //        var userRoles = await userManager.GetRolesAsync(user);

            //        var authClaims = new List<Claim>
            //    {
            //        new Claim(ClaimTypes.Name, user.UserName),
            //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    };

            //        foreach (var userRole in userRoles)
            //        {
            //            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            //        }

            //        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            //        var token = new JwtSecurityToken(
            //            issuer: _configuration["JWT:ValidIssuer"],
            //            audience: _configuration["JWT:ValidAudience"],
            //            expires: DateTime.Now.AddHours(3),
            //            claims: authClaims,
            //            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            //            );

            //        return Ok(new
            //        {
            //            token = new JwtSecurityTokenHandler().WriteToken(token),
            //            expiration = token.ValidTo
            //        });
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw (ex);
            //}
        }

        public async Task<IdentityResult> Register(UserMaster user, string password)
        {
            try
            {
                var result = await userManager.CreateAsync(user, password);
                if(result.Succeeded)
                { 
                    if (!await roleManager.RoleExistsAsync("Admin"))
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                    if (!await roleManager.RoleExistsAsync("Manager"))
                        await roleManager.CreateAsync(new IdentityRole("Manager"));
                    if (!await roleManager.RoleExistsAsync("LoanClerk"))
                        await roleManager.CreateAsync(new IdentityRole("LoanClerk"));
                    if (!await roleManager.RoleExistsAsync("Customer"))
                        await roleManager.CreateAsync(new IdentityRole("Customer"));

                    if (await roleManager.RoleExistsAsync("Customer"))
                    {
                        await userManager.AddToRoleAsync(user, "Customer");
                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
    }
}
