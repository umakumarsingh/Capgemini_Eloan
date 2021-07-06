using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace E_Loan.Entities
{
    public enum BillingStatus
    {
        [Display(Name = "Salaried Persond")]
        Salaried_Person = 1,
        [Display(Name = "Not Salaried Persond")]
        Not_Salaried_Person = 2
    }
}
