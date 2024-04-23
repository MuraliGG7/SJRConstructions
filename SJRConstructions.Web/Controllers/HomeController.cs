using System.Diagnostics;
using SJRConstructions.Core.Entities;
using SJRConstructions.Core.Interfaces;
using SJRConstructions.Web.Models;
using SJRConstructions.Web.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace SJRConstructions.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private UserProfile UserData;
        private readonly IUserRepository _userRepository;

        public HomeController(ILogger<HomeController> logger,IUserService userService, IUserRepository userRepository)
        {
            _logger = logger;
            _userService = userService;
            UserData = _userService.GetCurrentUser();
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            //if (UserData == null)
            //{
            //    return RedirectToAction("Login", "User");
            //}          
            return View();
        }

        public IActionResult About()
        {
            //if (UserData == null)
            //{
            //    return RedirectToAction("Login", "User");
            //}          
            return View();
        }
        public IActionResult Contact()
        {
            //if (UserData == null)
            //{
            //    return RedirectToAction("Login", "User");
            //}          
            return View();
        }
        public IActionResult Project()
        {
            //if (UserData == null)
            //{
            //    return RedirectToAction("Login", "User");
            //}          
            return View();
        }
        public IActionResult Service()
        {
            //if (UserData == null)
            //{
            //    return RedirectToAction("Login", "User");
            //}          
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}