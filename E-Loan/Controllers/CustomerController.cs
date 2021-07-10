using E_Loan.BusinessLayer.Interfaces;
using E_Loan.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Loan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerController : ControllerBase
    {
        /// <summary>
        /// Creating the field of ILoanCustomerServices and injecting in CustomerController constructor
        /// </summary>
        private readonly ILoanCustomerServices _customerServices;
        public CustomerController(ILoanCustomerServices loanCustomerServices)
        {
            _customerServices = loanCustomerServices;
        }
        /// <summary>
        /// Get loan status for customer while or before updating loan application.
        /// Get the loan application status by loanId
        /// Check if loan applcant email id is match with loan applied email id, then show loan record.
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("loan-status/{loanId}")]
        public async Task<ActionResult<LoanMaster>> AppliedLoanStatus(int loanId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Check if loan applcant email id is match with loan applied email id, then show loan record.
            var emailId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Get the loan application status by loanId
            var loanStatus = await _customerServices.AppliedLoanStatus(loanId);
            if (loanStatus == null)
            {
                return NotFound();
            }
            if (loanStatus.Email != emailId)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                { Status = "Error", Message = $"Loan Application Not found..." });
            }
            return loanStatus;
        }
        /// <summary>
        /// Applay new loan or mortage supply all value for LoanMaster
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("apply-mortage")]
        public async Task<IActionResult> ApplayMortage([FromBody] LoanMaster model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //get the login user email id and store in loan application
            var emailId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            LoanMaster newLoan = new LoanMaster
            {
                LoanName = model.LoanName,
                LoanAmount = model.LoanAmount,
                BusinessStructure = model.BusinessStructure,
                Billing_Indicator = model.Billing_Indicator,
                Tax_Indicator = model.Tax_Indicator,
                ContactAddress = model.ContactAddress,
                Phone = model.Phone,
                Email = emailId,//Pass registred user email id while apply loan
                AppliedBy = model.AppliedBy,
                CreatedOn = DateTime.Now,
                Status = model.Status
            };
            var result = await _customerServices.ApplyMortgage(newLoan);
            return Ok("Thanks for apply, Your Loan Id : " + result.LoanId);
        }
        /// <summary>
        /// Update loan/mortage if not recived by loan clerk then perforn actual update,
        /// Pass registred user email id while update loan
        /// Check loan status is recived return a error message
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loanId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update-mortage/{loanId}")]
        public async Task<IActionResult> UpdateMortage([FromBody] LoanMaster model, int loanId)
        {
            if (!ModelState.IsValid && loanId <= 0)
            {
                return BadRequest(ModelState);
            }
            LoanMaster loanUpdate = await _customerServices.AppliedLoanStatus(loanId);
            var emailId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Check if loan status is "Recived" not possible to update.
            if (loanUpdate.Status == LoanStatus.Received)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                { Status = "Error", Message = $"Loan Application Status = {loanUpdate.Status} cannot be Updated" });
            }

            //Update loan application if it is "not recived"
            if(loanUpdate != null && loanUpdate.Status == LoanStatus.NotReceived)
            {
                loanUpdate.LoanName = model.LoanName;
                loanUpdate.LoanAmount = model.LoanAmount;
                loanUpdate.BusinessStructure = model.BusinessStructure;
                loanUpdate.Billing_Indicator = model.Billing_Indicator;
                loanUpdate.Tax_Indicator = model.Tax_Indicator;
                loanUpdate.ContactAddress = model.ContactAddress;
                loanUpdate.Phone = model.Phone;
                loanUpdate.Email = emailId;//Pass registred user email id while update loan
                loanUpdate.AppliedBy = model.AppliedBy;
                loanUpdate.CreatedOn = DateTime.Now;
                loanUpdate.Status = model.Status;
                //Update loan application
                await _customerServices.UpdateMortgage(loanUpdate);
                return Ok(loanUpdate);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            { Status = "Error", Message = $"Loan Id with = {loanId} cannot be Updated" });
        }
    }
}
