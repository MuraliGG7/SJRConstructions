using GirlScoutCookieBoothManager.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GirlScoutCookieBoothManager.Core.Entities
{
    public class UserProfile
    {
        [Key]        
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Contact { get; set; }

        [ForeignKey("RoleID")]
        public Roles Role { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}
