using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJRConstructions.Core.Entities
{
    public class ProjectDetails
    {
        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZIPCode { get; set; }
        public string OwnerName { get; set; }
        public string OwnerContact { get; set; }
        public int StatusId { get; set; }
        public bool Active { get; set; }
    }
}
