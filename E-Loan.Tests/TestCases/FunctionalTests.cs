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
    public class FunctionalTests
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

        private readonly LoanMaster _loanMaster;
        private readonly UserMaster _userMaster;
        private readonly LoanProcesstrans _loanProcesstrans;
        private readonly LoanApprovaltrans _loanApprovaltrans;
        public FunctionalTests()
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
                LoanName ="Home Loan",
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
        static FunctionalTests()
        {
            if (!File.Exists("../../../../output_revised.txt"))
                try
                {
                    File.Create("../../../../output_revised.txt").Dispose();
                }
                catch (Exception)
                {

                }
            else
            {
                File.Delete("../../../../output_revised.txt");
                File.Create("../../../../output_revised.txt").Dispose();
            }
        }
        /// <summary>
        /// This Test is use for test the applied loan application status by LoanId 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_AppliedLoanStatusByLoanId()
        {
            //Arrange
            var res = false;
            //Action
            customerservice.Setup(repos => repos.AppliedLoanStatus(_loanMaster.LoanId)).ReturnsAsync(_loanMaster); ;
            var result = await _customerServices.AppliedLoanStatus(_loanMaster.LoanId);
            //Assertion
            if (result != null)
            {
                res = true;
            }
            //Assert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_AppliedLoanStatusByLoanId=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Add/Apply a loan/Mortage using this function testing below method or test case.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ApplayMortage()
        {
            //Arrange
            var res = false;
            //Action
            customerservice.Setup(repos => repos.ApplyMortgage(_loanMaster)).ReturnsAsync(_loanMaster);
            var result = _customerServices.ApplyMortgage(_loanMaster);
            //Assertion
            if (result != null)
            {
                res = true;
            }
            //Assert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_ApplayMortage=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Using this method try to  test mortage is updated or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_UpdateMortgage()
        {
            //Arrange
            bool res = false;
            var _updateLoan = new LoanMaster()
            {
                LoanId = 1,
                LoanName = "Personal-Loan",
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
            //Act
            customerservice.Setup(repo => repo.UpdateMortgage(_updateLoan)).ReturnsAsync(_updateLoan);
            var result = await _customerServices.UpdateMortgage(_updateLoan);
            if (result == _updateLoan)
            {
                res = true;
            }
            //Asert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_Validate_UpdateMortgage=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Using this method or test get all loan application
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> TestFor_GetAllLoanApplication()
        {
            //Arrange
            var res = false;
            //Action
            clerkservice.Setup(repos => repos.AllLoanApplication());
            var result = await _clerkServices.AllLoanApplication();
            //Assertion
            if (result != null)
            {
                res = true;
            }
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "TestFor_GetAllLoanApplication=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Using this test get all recived loan application
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> TestFor_GetRecivedLoanApplication()
        {
            //Arrange
            var res = false;
            //Action
            clerkservice.Setup(repos => repos.RecivedLoanApplication());
            var result = await _clerkServices.RecivedLoanApplication();
            //Assertion
            if (result != null)
            {
                res = true;
            }
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "TestFor_GetRecivedLoanApplication=" + res + "\n");
            return res;
        }
        /// <summary>
        /// using the below test try to get recived loan application
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> TestFor_GetNotRecivedLoanApplication()
        {
            //Arrange
            var res = false;
            //Action
            clerkservice.Setup(repos => repos.NotRecivedLoanApplication());
            var result = await _clerkServices.NotRecivedLoanApplication();
            //Assertion
            if (result != null)
            {
                res = true;
            }
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "TestFor_GetNotRecivedLoanApplication=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Using the below method try to test loan application is processed or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ProcessLoanApplication()
        {
            //Arrange
            var res = false;
            //Action
            clerkservice.Setup(repos => repos.ProcessLoan(_loanProcesstrans)).ReturnsAsync(_loanProcesstrans);
            var result = _clerkServices.ProcessLoan(_loanProcesstrans);
            //Assertion
            if (result != null)
            {
                res = true;
            }
            //Assert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_ProcessLoanApplication=" + res + "\n");
            return res;
        }
        /// <summary>
        /// using the below method try to test applied loan ststued is recived or not.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_AppliedLoanRecivedLoan_ornot()
        {
            //Arrange
            var res = false;
            //Action
            clerkservice.Setup(repos => repos.RecivedLoan(_loanMaster.LoanId)).ReturnsAsync(_loanMaster); ;
            var result = await _clerkServices.RecivedLoan(_loanMaster.LoanId);
            //Assertion
            if (result != null)
            {
                res = true;
            }
            //Assert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_AppliedLoanRecivedLoan_ornot=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Try to test for manager to get all accepted loan application
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> TestFor_GetManagerAllLoanApplication()
        {
            //Arrange
            var res = false;
            //Action
            managerservice.Setup(repos => repos.AllLoanApplication());
            var result = await _managerServices.AllLoanApplication();
            //Assertion
            if (result != null)
            {
                res = true;
            }
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "TestFor_GetManagerAllLoanApplication=" + res + "\n");
            return res;
        }
        /// <summary>
        /// using this test try to check and accept the loan application by manager with remark
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_AcceptLoanApplication_Manager()
        {
            //Arrange
            var res = false;
            //Action
            managerservice.Setup(repos => repos.AcceptLoanApplication(_loanMaster.LoanId, _loanMaster.ManagerRemark)).ReturnsAsync(_loanMaster); ;
            var result = await _managerServices.AcceptLoanApplication(_loanMaster.LoanId, _loanMaster.ManagerRemark);
            //Assertion
            if (result != null)
            {
                res = true;
            }
            //Assert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_AcceptLoanApplication_Manager=" + res + "\n");
            return res;
        }
        /// <summary>
        /// using this test try to check and reject the loan application by manager with remark
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_RejectLoanApplication_Manager()
        {
            //Arrange
            var res = false;
            //Action
            managerservice.Setup(repos => repos.RejectLoanApplication(_loanMaster.LoanId, _loanMaster.ManagerRemark)).ReturnsAsync(_loanMaster); ;
            var result = await _managerServices.RejectLoanApplication(_loanMaster.LoanId, _loanMaster.ManagerRemark);
            //Assertion
            if (result != null)
            {
                res = true;
            }
            //Assert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_RejectLoanApplication_Manager=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Using the below method try to test Sancationed loan is returining correct object or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_SanctionedLoanApplication()
        {
            //Arrange
            var res = false;
            //Action
            managerservice.Setup(repos => repos.SanctionedLoan(_loanApprovaltrans)).ReturnsAsync(_loanApprovaltrans);
            var result = _managerServices.SanctionedLoan(_loanApprovaltrans);
            //Assertion
            if (result != null)
            {
                res = true;
            }
            //Assert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_SanctionedLoanApplication=" + res + "\n");
            return res;
        }
        /// <summary>
        /// Check loan ststus for manager before starting loan Sancation process
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_CheckLoanStatus_ForManager()
        {
            //Arrange
            var res = false;
            //Action
            managerservice.Setup(repos => repos.CheckLoanStatus(_loanMaster.LoanId)).ReturnsAsync(_loanMaster); ;
            var result = await _managerServices.CheckLoanStatus(_loanMaster.LoanId);
            //Assertion
            if (result != null)
            {
                res = true;
            }
            //Assert
            //final result displaying in text file
            await File.AppendAllTextAsync("../../../../output_revised.txt", "Testfor_CheckLoanStatus=" + res + "\n");
            return res;
        }
    }
}