using System.ComponentModel.DataAnnotations;

namespace E_Loan.BusinessLayer
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
