using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GirlScoutCookieBoothManager.Core.Entities
{
    public class EmailService
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string InviteToken { get; set; }
        public string Type { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}
