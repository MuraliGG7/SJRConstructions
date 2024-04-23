using System.ComponentModel.DataAnnotations;

namespace SJRConstructions.Web.ViewModels
{
    public class ForgotPasswordVM
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string? InviteToken { get; set; }
        public string? Type { get; set; }
    }
}
