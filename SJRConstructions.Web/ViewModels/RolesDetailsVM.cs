using System.ComponentModel.DataAnnotations;

namespace SJRConstructions.Web.ViewModels
{
    public class RolesDetailsVM
    {
        public int RoleID { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
