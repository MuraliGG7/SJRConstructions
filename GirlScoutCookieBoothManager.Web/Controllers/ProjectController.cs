using AutoMapper;
using GirlScoutCookieBoothManager.Core.Entities;
using GirlScoutCookieBoothManager.Core.Interfaces;
using GirlScoutCookieBoothManager.Web.Enums;
using GirlScoutCookieBoothManager.Web.Services.User;
using GirlScoutCookieBoothManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GirlScoutCookieBoothManager.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserService _userService;
        private UserProfile UserData;

        public ProjectController(IMapper mapper, IUserService userService, IUserRepository userRepository, IProjectRepository projectRepository)
        {
            this.mapper = mapper;
            _userService = userService;
            UserData = _userService.GetCurrentUser();
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }
        public async Task<IActionResult> Index()
        {
            if (UserData == null)
            {
                return RedirectToAction("Login", "User");
            }
            UserRole userRole = (UserRole)UserData.RoleID;
            if (userRole == UserRole.TroopCoordinator)
            {
                TempData["AccessMessage"] = "You dont have permission to access the Page.";
                return View("NoPermissions");
            }
            var projectDetails = mapper.Map<List<ProjectDetailsVM>>(await _projectRepository.GetAllProjects());

            return View(projectDetails);
        }

        public async Task<IActionResult> Create()
        {
            if (UserData == null)
            {
                return RedirectToAction("Login", "User");
            }
            UserRole userRole = (UserRole)UserData.RoleID;
            if (userRole == UserRole.TroopCoordinator)
            {
                TempData["AccessMessage"] = "You dont have permission to access the Page.";
                return View("NoPermissions");
            }
            ProjectDetailsVM projectDetailsVM = new ProjectDetailsVM();
            return View(projectDetailsVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectDetailsVM projectVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Map the ViewModel to the corresponding entity
                    var project = mapper.Map<ProjectDetails>(projectVM);
                    //project.StatusId = projectVM.Status;
                    // Save the project entity to the database
                    var result = await _projectRepository.CreateLocationAsync(project);

                    // Assuming projectRepository.CreateProjectAsync returns the created project
                     if (result != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to create project.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ModelState.AddModelError(string.Empty, "An error occurred while creating the project.");
                return View(projectVM);
            }
            return View(projectVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (UserData == null)
            {
                return RedirectToAction("Login", "User");
            }
            UserRole userRole = (UserRole)UserData.RoleID;
            if (userRole == UserRole.TroopCoordinator)
            {
                TempData["AccessMessage"] = "You dont have permission to access the Page.";
                return View("NoPermissions");
            }
            if (id == null)
            {
                return NotFound();
            }
            var project = await _projectRepository.GetProjectsById(id);
            if (project == null)
            {
                return NotFound();
            }
            var projectDetails = mapper.Map<ProjectDetailsVM>(project);

            return View(projectDetails);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProjectDetailsVM projectVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Map the ViewModel to the corresponding entity
                    var project = mapper.Map<ProjectDetails>(projectVM);
                    //project.StatusId = projectVM.Status;
                    // Save the project entity to the database
                    var result = await _projectRepository.UpdateLocationAsync(project);

                    // Assuming projectRepository.CreateProjectAsync returns the created project
                    if (result != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to create project.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ModelState.AddModelError(string.Empty, "An error occurred while creating the project.");
                return View(projectVM);
            }
            return View(projectVM);
        }

    }
}
