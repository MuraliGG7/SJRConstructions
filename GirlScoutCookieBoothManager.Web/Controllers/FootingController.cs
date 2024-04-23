using GirlScoutCookieBoothManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GirlScoutCookieBoothManager.Web.Controllers
{
    public class FootingController : Controller
    {
        public IActionResult Index()
        {
            var model = new CubicCalculationVM();
            return View(model);
        }
    }
}
