using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace E_Loan.Entities
{
    public class LoanApprovaltrans
    {
        /// <summary>
        /// This class to use for approve the loan by manager
        /// </summary>
        public LoanApprovaltrans()
        {
            double intrestRate = .06;
            double paymentAmount = (SanctionedAmount) * 
                Math.Pow((1 + intrestRate / 100),Termofloan);

            double monthlyPay = paymentAmount / Termofloan;
            MonthlyPayment = monthlyPay;
            //Calculate Loan Closer Date
            double termdate = (Termofloan * 365);
            DateTime closerDate = PaymentStartDate.AddDays(termdate);
            LoanCloserDate = closerDate;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Sanctioned Amount")]
        public double SanctionedAmount { get; set; }
        [Display(Name = "Term Plan in Year")]
        public double Termofloan { get; set; }
        [Display(Name = "Payment Start Date")]
        public DateTime PaymentStartDate { get; set; }
        [Display(Name = "Loan Closer Date")]
        public DateTime LoanCloserDate { get; set; }
        [Display(Name = "Monthly Payment")]
        public double MonthlyPayment { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
