using System.ComponentModel.DataAnnotations;

namespace E_Loan.BusinessLayer
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }

        ///Below mention all role exists in database.
        //public const string Admin = "Admin";
        //public const string Manager = "Manager";
        //public const string LoanClerk = "Clerk";
        //public const string Customer = "Customer";
    }
}