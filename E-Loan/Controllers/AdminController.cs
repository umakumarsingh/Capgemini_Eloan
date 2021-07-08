using E_Loan.BusinessLayer;
using E_Loan.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Loan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<UserMaster> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        //private readonly ILoanAdminServices _adminServices;

        public AdminController(UserManager<UserMaster> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            //_adminServices = loanAdminServices;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            //Check if user is disable or Enable
            if (user.Enabled == true)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Id exists but locked, Contact Admin!" });

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserMasterRegisterModel model)
        {
            var userExists = await userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            UserMaster user = new UserMaster()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Name,
                Enabled = model.Enabled,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
        [HttpPost]
        [Route("create-role")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleViewModel model)
        {
            var roleExists = await roleManager.FindByNameAsync(model.RoleName);
            if (roleExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Role already exists!" });

            IdentityRole identityRole = new IdentityRole { Name = model.RoleName.Trim() };
            IdentityResult result = await roleManager.CreateAsync(identityRole);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Role creation failed! Please check user role details and try again." });

            return Ok(new Response { Status = "Success", Message = "Role created successfully!" });
        }

        [HttpPost]
        [Route("editusers-role/{roleId}")]
        public async Task<IActionResult> EditUsersInRole([FromBody] UserRoleViewModel model, string roleId)
        {
            IdentityResult result = null;
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                { Status = "Error", Message = $"Role With Id = {roleId} cannot be found" });
            }
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

                if (!result.Succeeded)
                {
                    //return RedirectToAction("EditRole", new { Id = roleId });
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response
                    {
                        Status = "Error",
                        Message = "User creation failed! Please check user details and try again."
                    });
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
            return Ok(new Response { Status = "Success", Message = "User Role Updation successfully!" });
        }

        [HttpPost]
        [Route("edit-user")]
        public async Task<IActionResult> EditUsers([FromBody] EditUsersViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                { Status = "Error", Message = $"User with Eamil = {model.Email} cannot be found" });
            }
            else
            {
                //to change password Name/UserName and email shoud not change only password can supplay new.
                //user.UserName = model.Name;
                //user.Email = model.Email;
                user.PasswordHash = model.Password;
                user.Contact = model.Contact;
                user.Address = model.Address;
                var result = await userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response
                    {
                        Status = "Error",
                        Message = "Update User failed! Please check user details and try again."
                    });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Ok(new Response { Status = "Success", Message = "User Updation successfully!" });
        }

        [HttpPost]
        [Route("edit-role")]
        public async Task<IActionResult> EditRole([FromBody] EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                { Status = "Error", Message = $"Role With Id = {model.Id} cannot be found" });
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole", "Admin");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return Ok(new Response { Status = "Success", Message = "User Role Edited successfully!" });
            }
        }

        [HttpGet]
        [Route("list-role")]
        public async Task<IEnumerable<IdentityRole>> ListRole()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return roles;
        }

        [HttpGet]
        [Route("list-users")]
        public async Task<IEnumerable<UserMaster>> ListUser()
        {
            var users = await userManager.Users.ToListAsync();
            return users;
        }
        
        [HttpPost]
        [Route("disable-user/{userId}")]
        public async Task<IActionResult> DisableUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            //Check if user is disable or Enable
            bool newStatus = true;
            if (user.Enabled == true)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Id exists but locked, Contact Admin!" });
            }
            else
            {
                user.Enabled = newStatus;
                var result = await userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response
                    { Status = "Error", Message = $"User Not disable = {user.Email} contact admin" });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                //return Ok(new Response { Status = "Success", Message = "User disabled successfully!" });
            }
            return Ok(new Response { Status = "Success", Message = "User disabled successfully!" });
        }

        [HttpPost]
        [Route("Enable-user/{userId}")]
        public async Task<IActionResult> EnableeUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            //Check if user is disable or Enable
            bool newStatus = false;
            if (user.Enabled == true)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Id exists but locked, Contact Admin!" });
            }
            else
            {
                user.Enabled = newStatus;
                var result = await userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response
                    { Status = "Error", Message = $"User Not Enabled = {user.Email} contact admin" });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                //return Ok(new Response { Status = "Success", Message = "User disabled successfully!" });
            }
            return Ok(new Response { Status = "Success", Message = "User Enable successfully!" });
        }
        
        //Edit multi user role and provide tehm new role.
        //[HttpPost]
        //[Route("editmultiusers-role/{roleId}")]
        //public async Task<IActionResult> EditMultiUsers-InRole(List<UserRoleViewModel> model, string roleId)
        //{
        //    var role = await roleManager.FindByIdAsync(roleId);
        //    if (role == null)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response
        //        { Status = "Error", Message = $"Role With Id = {roleId} cannot be found" });
        //    }
        //    for (int i = 0; i < model.Count; i++)
        //    {
        //        var user = await userManager.FindByIdAsync(model[i].UserId);
        //        IdentityResult result = null;

        //        if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
        //        {
        //            result = await userManager.AddToRoleAsync(user, role.Name);
        //        }
        //        else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
        //        {
        //            result = await userManager.RemoveFromRoleAsync(user, role.Name);
        //        }
        //        else
        //        {
        //            continue;
        //        }
        //        if (result.Succeeded)
        //        {
        //            if (i < model.Count - 1)
        //                continue;
        //            else
        //                return RedirectToAction("EditRole", new { Id = roleId });
        //        }
        //    }

        //    return RedirectToAction("EditRole", new { Id = roleId });
        //}

        //[HttpPost]
        //[Route("register-admin")]
        //public async Task<IActionResult> RegisterAdmin([FromBody] UserMasterRegisterModel model)
        //{
        //    var userExists = await userManager.FindByEmailAsync(model.Email);
        //    if (userExists != null)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

        //   UserMaster user = new UserMaster()
        //    {
        //        Email = model.Email,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = model.Name
        //    };
        //    var result = await userManager.CreateAsync(user, model.Password);
        //    if (!result.Succeeded)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        //    if (!await roleManager.RoleExistsAsync(CreateRoleViewModel.Admin))
        //        await roleManager.CreateAsync(new IdentityRole(CreateRoleViewModel.Admin));
        //    if (!await roleManager.RoleExistsAsync(CreateRoleViewModel.Manager))
        //        await roleManager.CreateAsync(new IdentityRole(CreateRoleViewModel.Manager));
        //    if (!await roleManager.RoleExistsAsync(CreateRoleViewModel.LoanClerk))
        //        await roleManager.CreateAsync(new IdentityRole(CreateRoleViewModel.LoanClerk));
        //    if (!await roleManager.RoleExistsAsync(CreateRoleViewModel.Customer))
        //        await roleManager.CreateAsync(new IdentityRole(CreateRoleViewModel.Customer));

        //    if (await roleManager.RoleExistsAsync(CreateRoleViewModel.Admin))
        //    {
        //        await userManager.AddToRoleAsync(user, CreateRoleViewModel.Admin);
        //    }

        //    return Ok(new Response { Status = "Success", Message = "AdminUser created successfully!" });
        //}
    }
}
