using SJRConstructions.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace SJRConstructions.Web.ViewModels
{
    public class UserDetailsVM
    {       

        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Display(Name = "Contact")]
        public string Contact { get; set; }
        
        // Add more properties for other levels if needed
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
