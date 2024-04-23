using System.ComponentModel.DataAnnotations;

namespace GirlScoutCookieBoothManager.Web.ViewModels
{
    public class InvitationVM
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
