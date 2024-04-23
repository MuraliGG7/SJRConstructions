using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GirlScoutCookieBoothManager.Core.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZIPCode { get; set; }
        public string Restriction { get; set; }
        public string HighDemand { get; set; }
        public string Status { get; set; }        
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}
