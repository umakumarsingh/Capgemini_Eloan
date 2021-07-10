using E_Loan.BusinessLayer.Interfaces;
using E_Loan.BusinessLayer.ViewModels;
using E_Loan.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Loan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager,Admin")]
    public class ManagerController : ControllerBase
    {
        /// <summary>
        /// Creating the field of ILoanManagerServices and injecting in ManagerController constructor
        /// </summary>
        private readonly ILoanManagerServices _managerServices;
        public ManagerController(ILoanManagerServices loanManagerServices)
        {
            _managerServices = loanManagerServices;
        }
        /// <summary>
        /// Get all application details for manager
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<LoanMaster>> GetAllApplication()
        {
            return await _managerServices.AllLoanApplication();
        }
        /// <summary>
        /// Accept loan application and add remark on that, using this end point loan status is changed to "Accept"
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("accept/{loanId}/{remark}")]
        public async Task<LoanMaster> AcceptLoanApplication(int loanId, string remark)
        {
            return await _managerServices.AcceptLoanApplication(loanId, remark);
        }
        /// <summary>
        /// Reject loan application and add remark on that, using this end point loan status is changed to "Reject"
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("reject/{loanId}/{remark}")]
        public async Task<LoanMaster> RejectLoanApplication(int loanId, string remark)
        {
            return await _managerServices.RejectLoanApplication(loanId, remark);
        }
        /// <summary>
        /// Start the loan Sanction if all status and checked is passed.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loanId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("sanctioned-loan/{loanId}")]
        public async Task<IActionResult> SanctionedLoan([FromBody] LoanApprovalViewModel model, int loanId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Before Sanctioned the loan for loanApplication Id - 
            //Make sure Loan Applcation status is in "Accept" mode.
            var loanStatus = await _managerServices.CheckLoanStatus(loanId);
            if(loanStatus.Status == LoanStatus.Accept)
            {
                LoanApprovaltrans newApproved = new LoanApprovaltrans
                {
                    SanctionedAmount = model.SanctionedAmount,
                    Termofloan = model.Termofloan,
                    PaymentStartDate = model.PaymentStartDate
                };
                var result = await _managerServices.SanctionedLoan(newApproved);
                return Ok("Your Loan is Sanctioned, Your Loan process Id : " + result.Id);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            { Status = "Error", Message = $"Loan Id with = {loanId} cannot be Processed" });
        }
    }
}
