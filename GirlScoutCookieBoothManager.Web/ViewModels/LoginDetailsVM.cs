﻿using System.ComponentModel.DataAnnotations;

namespace GirlScoutCookieBoothManager.Web.ViewModels
{
    public class LoginDetailsVM
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}
