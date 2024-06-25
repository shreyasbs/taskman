using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMan.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Compare("ConfirmPassword", ErrorMessage = "Password and Confirm Password should match")]
        public string Password { get; set; }
        [Required(ErrorMessage = " Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password should match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
    }
}
