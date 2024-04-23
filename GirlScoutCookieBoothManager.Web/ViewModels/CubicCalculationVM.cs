using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GirlScoutCookieBoothManager.Web.ViewModels
{
    public class CubicCalculationVM
    {
        [Required(ErrorMessage = "Please select a footing type.")]
        [Display(Name = "Footing Type")]
        public string FootingType { get; set; }

        public List<SelectListItem> AvailableFootingTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "SFT1" },
            new SelectListItem { Value = "2", Text = "SFT2" },
            new SelectListItem { Value = "3", Text = "CFT1" },
            new SelectListItem { Value = "4", Text = "CFT2" },
            new SelectListItem { Value = "5", Text = "CFT3" },
            new SelectListItem { Value = "6", Text = "CFT4" },
            new SelectListItem { Value = "7", Text = "CFT5" },
            new SelectListItem { Value = "8", Text = "CFT6" }
            // Add more items as needed
        };
        public string SteelRodType { get; set; }
        public List<SelectListItem> AvailableSteelRodTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "8mm" },
            new SelectListItem { Value = "2", Text = "10mm" },
            new SelectListItem { Value = "3", Text = "12mm" },
            new SelectListItem { Value = "4", Text = "16mm" },
            new SelectListItem { Value = "5", Text = "20mm" },
            new SelectListItem { Value = "5", Text = "25mm" },
            new SelectListItem { Value = "6", Text = "32mm" }
            // Add more items as needed
        };
        public double Length { get; set; }
        public double Breath { get; set; }
        public double Height { get; set; }
        public double CubicFoot { get; set; }
        public double CubicMeters { get; set; }
        public double C2CA { get; set; }
        public double C2CB { get; set; }
        public double TotalRods { get; set; }
        public double TotalRodsWeight { get; set; }
    }

}
