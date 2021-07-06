using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace E_Loan.Entities
{
    public enum TaxStatus
    {
        
        [Display(Name = "Tax Payer")]
        Tax_Payer,
        [Display(Name = "Not Tax Payer")]
        Not_tax_Payer
    }
}
