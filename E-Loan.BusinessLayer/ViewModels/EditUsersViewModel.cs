using System.ComponentModel.DataAnnotations;

namespace E_Loan.BusinessLayer
{
    public class EditUsersViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
    }
}
