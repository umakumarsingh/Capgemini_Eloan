using E_Loan.BusinessLayer.Interfaces;
using E_Loan.BusinessLayer.Services;
using E_Loan.BusinessLayer.Services.Repository;
using E_Loan.Entities;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace E_Loan.Tests.TestCases
{
    public class ExceptionalTest
    {
        /// <summary>
        /// Creating Referance Variable and Mocking repository class
        /// </summary>
        private readonly ILoanCustomerServices _customerServices;
        private readonly ILoanClerkServices _clerkServices;
        private readonly ILoanManagerServices _managerServices;
        public readonly Mock<ILoanCustomerRepository> customerservice = new Mock<ILoanCustomerRepository>();
        public readonly Mock<ILoanClerkRepository> clerkservice = new Mock<ILoanClerkRepository>();
        public readonly Mock<ILoanManagerRepository> managerservice = new Mock<ILoanManagerRepository>();

        private LoanMaster _loanMaster;
        private UserMaster _userMaster;
        private LoanProcesstrans _loanProcesstrans;
        private LoanApprovaltrans _loanApprovaltrans;
        public ExceptionalTest()
        {
            /// <summary>
            /// Injecting service object into Test class constructor
            /// </summary>
            _customerServices = new LoanCustomerServices(customerservice.Object);
            _clerkServices = new LoanClerkServices(clerkservice.Object);
            _managerServices = new LoanManagerServices(managerservice.Object);
            _loanMaster = new LoanMaster
            {
                LoanId = 1,
                LoanName = "Home Loan",
                Date = System.DateTime.Now,
                BusinessStructure = BusinessStatus.Individual,
                Billing_Indicator = BillingStatus.Not_Salaried_Person,
                Tax_Indicator = TaxStatus.Not_tax_Payer,
                ContactAddress = "Gaya-Bihar",
                Phone = "9632584754",
                Email = "eloan@iiht.com",
                AppliedBy = "Kumar",
                CreatedOn = DateTime.Now,
                ManagerRemark = "Ok",
                Status = LoanStatus.NotRecived
            };
            _userMaster = new UserMaster
            {

            };
            _loanProcesstrans = new LoanProcesstrans
            {
                Id = 1,
                AcresofLand = 1,
                LandValueinRs = 4700000,
                AppraisedBy = "Uma",
                ValuationDate = DateTime.Now,
                AddressofProperty = "Mall - Karnataka",
                SuggestedAmount = 4000000,
                ManagerId = 3,
                LoanId = 1
            };
            _loanApprovaltrans = new LoanApprovaltrans
            {
                Id = 1,
                SanctionedAmount = 4000000,
                Termofloan = 72,
                PaymentStartDate = DateTime.Now,
                LoanCloserDate = DateTime.Now,
                MonthlyPayment = 3330000
            };
        }
        /// <summary>
        /// Creating test output text file that store the result in boolean result
        /// </summary>
        static ExceptionalTest()
        {
            if (!File.Exists("../../../../output_exception_revised.txt"))
                try
                {
                    File.Create("../../../../output_exception_revised.txt").Dispose();
                }
                catch (Exception)
                {

                }
            else
            {
                File.Delete("../../../../output_exception_revised.txt");
                File.Create("../../../../output_exception_revised.txt").Dispose();
            }
        }
        /// <summary>
        /// Test to validate if user pass the null object while apply mortage, return null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_InvlidApplyMortage()
        {
            //Arrange
            bool res = false;
            _loanMaster = null;
            //Act
            customerservice.Setup(repo => repo.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster = null);
            var result = await _customerServices.ApplyMortgage(_loanMaster);
            if (result == null)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_Validate_InvlidApplyMortage=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate if clerk pass the null object while Process Loan, return null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_InvlidProcessLoanTrans()
        {
            //Arrange
            bool res = false;
            _loanProcesstrans = null;
            //Act
            clerkservice.Setup(repo => repo.ProcessLoan(_loanProcesstrans)).ReturnsAsync(_loanProcesstrans = null);
            var result = await _clerkServices.ProcessLoan(_loanProcesstrans);
            if (result == null)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_Validate_InvlidProcessLoanTrans=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Test to validate if clerk pass the null object while Sanctioned Loan, return null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_InvlidSanctionedLoanTrans()
        {
            //Arrange
            bool res = false;
            _loanApprovaltrans = null;
            //Act
            managerservice.Setup(repo => repo.SanctionedLoan(_loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans = null);
            var result = await _managerServices.SanctionedLoan(_loanApprovaltrans);
            if (result == null)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_exception_revised.txt", "Testfor_Validate_InvlidSanctionedLoanTrans=" + res + "\n");
            return res;
        }
    }
}
