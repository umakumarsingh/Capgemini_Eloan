using E_Loan.BusinessLayer.Interfaces;
using E_Loan.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Loan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClerkController : ControllerBase
    {
        /// <summary>
        /// Creating the field of ILoanClerkServices and injecting in ClerkController constructor
        /// </summary>
        private readonly ILoanClerkServices _clerkServices;
        public ClerkController(ILoanClerkServices loanClerkServices)
        {
            _clerkServices = loanClerkServices;
        }
        /// <summary>
        /// call the default get method to get all loan application
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<LoanMaster>> GetAllApplication()
        {
            return await _clerkServices.AllLoanApplication();
        }
        /// <summary>
        /// See the status of not recived application
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("not-recived")]
        public async Task<IEnumerable<LoanMaster>> NotRecivedLoanApplication()
        {
            return await _clerkServices.NotRecivedLoanApplication();
        }
        /// <summary>
        /// Show/Get the status and list of recived loan application
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("recived")]
        public async Task<IEnumerable<LoanMaster>> RecivedLoanApplication()
        {
            return await _clerkServices.RecivedLoanApplication();
        }
        /// <summary>
        /// Start the loan process after verify, //Make sure loan status is "recived" before process loan application
        /// //Start Process loan adding if loan status is "Recived"
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loanId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("process-loan/{loanId}")]
        public async Task<IActionResult> ProcessLoan([FromBody] LoanProcesstrans model, int loanId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Make sure loan status is "recived" before process loan application
            var loanStatus = await _clerkServices.RecivedLoan(loanId);
            //Process loan adding with below attribute with loan Id
            if (loanStatus.Status == LoanStatus.Recived)
            {
                LoanProcesstrans newProcess = new LoanProcesstrans
                {
                    AcresofLand = model.AcresofLand,
                    LandValueinRs = model.LandValueinRs,
                    AppraisedBy = model.AppraisedBy,
                    ValuationDate = model.ValuationDate,
                    AddressofProperty = model.AddressofProperty,
                    SuggestedAmount = model.SuggestedAmount,
                    ManagerId = model.ManagerId,
                    LoanId = model.LoanId //Need TO supply
                };
                var result = await _clerkServices.ProcessLoan(newProcess);
                return Ok("Your Loan in Process sent to manager, Your Loan process Id : " + result.Id);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            { Status = "Error", Message = $"Loan Id with = {loanId} cannot be Processed" });
        }
    }
}
