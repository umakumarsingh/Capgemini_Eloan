using E_Loan.BusinessLayer.Interfaces;
using E_Loan.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace E_Loan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        /// Get loan status for customer while or before updating loan application
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
            var loanStatus = await _customerServices.AppliedLoanStatus(loanId);

            if (loanStatus == null)
            {
                return NotFound();
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
            LoanMaster newLoan = new LoanMaster
            {
                LoanName = model.LoanName,
                LoanAmount = model.LoanAmount,
                BusinessStructure = model.BusinessStructure,
                Billing_Indicator = model.Billing_Indicator,
                Tax_Indicator = model.Tax_Indicator,
                ContactAddress = model.ContactAddress,
                Phone = model.Phone,
                Email = model.Email,
                AppliedBy = model.AppliedBy,
                CreatedOn = DateTime.Now,
                Status = model.Status
            };
            var result = await _customerServices.ApplyMortgage(newLoan);
            return Ok("Thanks for apply, Your Loan Id : " + result.LoanId);
        }
        /// <summary>
        /// Update loan/mortage if not recived by loan clerk then perforn actual update
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

            //Check if loan status is "Recived" not possible to update.
            if(loanUpdate.Status == LoanStatus.Recived)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                { Status = "Error", Message = $"Loan Application Status = {loanUpdate.Status} cannot be Updated" });
            }

            //Update loan application if it is "not recived"
            if(loanUpdate != null && loanUpdate.Status == LoanStatus.NotRecived)
            {
                loanUpdate.LoanName = model.LoanName;
                loanUpdate.LoanAmount = model.LoanAmount;
                loanUpdate.BusinessStructure = model.BusinessStructure;
                loanUpdate.Billing_Indicator = model.Billing_Indicator;
                loanUpdate.Tax_Indicator = model.Tax_Indicator;
                loanUpdate.ContactAddress = model.ContactAddress;
                loanUpdate.Phone = model.Phone;
                loanUpdate.Email = model.Email;
                loanUpdate.AppliedBy = model.AppliedBy;
                loanUpdate.CreatedOn = DateTime.Now;
                loanUpdate.Status = model.Status;
                
                await _customerServices.UpdateMortgage(loanUpdate);
                return Ok(loanUpdate);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            { Status = "Error", Message = $"Loan Id with = {loanId} cannot be Updated" });
        }
    }
}
