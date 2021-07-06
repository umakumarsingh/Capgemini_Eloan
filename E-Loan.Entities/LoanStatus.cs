using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace E_Loan.Entities
{
    /// <summary>
    /// This Enum define key value pair for Loan Status. its bind on Customer loan Status
    /// </summary>
    public enum LoanStatus
    {
        [Display(Name = "Not Recived")]
        NotRecived = 1,
        Recived = 2,
        Accept = 3,
        Rejected =4
    }
}
