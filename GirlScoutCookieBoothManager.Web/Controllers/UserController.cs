using GirlScoutCookieBoothManager.Core.Entities;
using GirlScoutCookieBoothManager.Infrastructure;
using GirlScoutCookieBoothManager.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GirlScoutCookieBoothManager.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using GirlScoutCookieBoothManager.Infrastructure.Repositories;
using System.Configuration;
using System.Text;
using AutoMapper;
using GirlScoutCookieBoothManager.Web.Services.EmailService;
using GirlScoutCookieBoothManager.Web.Services.User;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using GirlScoutCookieBoothManager.Web.Enums;

namespace GirlScoutCookieBoothManager.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailRepository _emailRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IUserService _userService;
        private UserProfile UserData;


        public UserController(IUserRepository userRepository, IEmailRepository emailRepository, IConfiguration configuration, IMapper mapper,
            IEmailSender emailSender, IUserService userService)
        {
            _userRepository = userRepository;
            _emailRepository = emailRepository;
            _configuration = configuration;
            _mapper = mapper;
            _emailSender = emailSender;
            _userService = userService;
            UserData = _userService.GetCurrentUser();
        }


        // GET: /User/Register
        [HttpGet]
        public IActionResult Register(string? Id = null)
        {
            UserDetailsVM userDetails = new UserDetailsVM();
            if (Id != null)
            {
                var data = _emailRepository.GetEmailByGuId(Id);
                userDetails = new UserDetailsVM
                {
                    Email = data?.Email ?? string.Empty
                };
            }

            return View(userDetails);
        }
        [HttpGet]
        public IActionResult UpdatePassword(string? Id = null)
        {
            UpdatePasswordVM UpdatePasswordDetails = new UpdatePasswordVM();
            if (Id != null)
            {
                var data = _emailRepository.GetEmailByGuId(Id);
                UpdatePasswordDetails = new UpdatePasswordVM
                {
                    Email = data?.Email ?? string.Empty
                };
            }

            return View(UpdatePasswordDetails);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult NoPermissions()
        {
            return View("NoPermissions");
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MySigninInfo");
            return RedirectToAction("Index", "Home"); // Change this to your desired redirect route
        }

        [HttpGet]
        public IActionResult Profile()
        {
            if (UserData == null)
            {
                return RedirectToAction("Login", "User");
            }            
            
            ProfileDetailsVM ProfileDetails = new ProfileDetailsVM();
            ProfileDetails.UserDetails = new UserDetailsVM();
            ProfileDetails.UserDetails.Email = UserData.Email;
            ProfileDetails.UserDetails.Contact = UserData.Contact;
            return View(ProfileDetails);
        }
        // POST: /User/Register
        [HttpPost]
        public IActionResult Register(UserDetailsVM model)
        {

            if (ModelState.IsValid)
            {
                // Check if the email already exists in the database
                if (_userRepository.IsEmailInUse(model.Email))
                {
                    ModelState.AddModelError("Email", "Email is already in use");
                    return View(model); // Return to the registration page with an error message
                }

                var passwordHasher = new PasswordHasher<UserDetailsVM>();
                var hashedPassword = passwordHasher.HashPassword(model, model.Password);
                // Map the model data to your User entity and save it
                var user = new UserProfile
                {
                    Email = model.Email,
                    Password = hashedPassword,
                    Contact = model.Contact,
                    RoleID = 3

                };

                _userRepository.Register(user); // Implement this method in your UserService
                return RedirectToAction("Index", "Home"); // Redirect to home page after registration
            }

            return View(model); // Return to the registration page with validation errors
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDetailsVM model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the user details from your repository or database
                var user = _userRepository.GetUserByEmail(model.Email);

                if (user != null)
                {                    
                    // Compare the hashed password stored in the database with the provided password
                    var passwordHasher = new PasswordHasher<UserProfile>();
                    var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

                    if (passwordVerificationResult == PasswordVerificationResult.Success)
                    {
                        UserRole userRole = (UserRole)user.RoleID;
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                            new Claim(ClaimTypes.Role,userRole.ToString()),
                            new Claim(ClaimTypes.UserData,user.RoleID.ToString()),
                            new Claim(ClaimTypes.GivenName,userRole.ToString()),
                            new Claim(ClaimTypes.Email,user.Email.ToString())
                        };

                        var identity = new ClaimsIdentity(claims, "MySigninInfo");
                        var principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync("MySigninInfo", principal);
                        TempData["RoleID"] = user.RoleID;
                        return RedirectToAction("Index", "Home");
                    }
                }

                // If authentication fails, add a model error and return to the login page
                ModelState.AddModelError(string.Empty, "Invalid credentials");
                return View(model);
            }

            return View(model); // Return to the registration page with validation errors
        }
        [HttpPost]
        public IActionResult UpdateProfileData(ProfileDetailsVM model)
        {
            if (model.UserDetails != null)
            {

                var user = _userRepository.GetUserById(UserData.UserID.ToString()); // Get the user by ID
                user.Contact = model.UserDetails.Contact;               
                    _userRepository.Update(user); // Implement this method in your UserService
                    TempData["ProfileMessage"] = "Profile details updated successfully!";
                return RedirectToAction("Profile", "User"); // Redirect to home page after profile update
            }

            return RedirectToAction("Profile", "User"); // Return to the profile page with validation errors
        }
        [HttpPost]
        public IActionResult ChangePassword(ProfileDetailsVM model)
        {
            if (model.ChangePassword != null)
            {
                var user = _userRepository.GetUserById(UserData.UserID.ToString()); // Get the user by ID

                // Verify the current password
                var passwordHasher = new PasswordHasher<UserProfile>();
                var passwordVerification = passwordHasher.VerifyHashedPassword(user, user.Password, model.ChangePassword.CurrentPassword);

                if (passwordVerification != PasswordVerificationResult.Success)
                {
                    ModelState.AddModelError("CurrentPassword", "Invalid current password");
                    return View(model); // Return to the change password page with an error message
                }

                // Hash and update the new password
                var hashedPassword = passwordHasher.HashPassword(user, model.ChangePassword.NewPassword);
                user.Password = hashedPassword;

                _userRepository.UpdatePassword(user); // Implement this method in your UserService
                TempData["PasswordMessage"] = "Password changed successfully!";
                return RedirectToAction("Profile", "User"); // Redirect to home page after password change
            }

            return RedirectToAction("Profile", "User"); // Return to the change password page with validation errors
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ForgotPasswordVM model)
        {
            TempData["UsernotFound"] = null;
            var user = _userRepository.GetUserByEmail(model.Email);
            if (user == null)
            {
                TempData["UsernotFound"] = "The Reset Password details have been send to the email address provided.";
                return RedirectToAction("ForgotPassword", "User");
            }
            model.InviteToken = Guid.NewGuid().ToString();
            model.Type = "ForgotPassword";
            var emailServices = new EmailService
            {
                InviteToken = model.InviteToken,
                Type = model.Type,
                Email = model.Email,
            };

            await _emailRepository.SaveInvitationAsync(emailServices);


            string Email = model.Email;

            if (!string.IsNullOrWhiteSpace(Email))
            {

                StringBuilder registerEmailBody = GetEmailContent(model.InviteToken);

                var message = new Message(new string[] { Email }, "Forgot Password", registerEmailBody.ToString(), null);
                await _emailSender.SendEmailAsync(message);
            }
            return RedirectToAction("", "Home");

        }
        [HttpPost]
        public IActionResult UpdatePassword(UpdatePasswordVM model)
        {
            var user = _userRepository.GetUserByEmail(model.Email);
            var passwordHasher = new PasswordHasher<UpdatePasswordVM>();
            var hashedPassword = passwordHasher.HashPassword(model, model.NewPassword);
            user.Password = hashedPassword;

            _userRepository.UpdatePassword(user); // Implement this method in your UserService
            TempData["PasswordMessage"] = "Password changed successfully!";
            return RedirectToAction("Profile", "User");
        }
        private StringBuilder GetEmailContent(string token)
        {
            string forgotPassword = _configuration.GetValue<string>("UserInformation:Forgotpassword") + token;
            StringBuilder emailBody = new StringBuilder();
            emailBody.Append("<html>");
            emailBody.Append("<head>");
            emailBody.Append("<style>");
            emailBody.Append("body { font-family: Arial, sans-serif; }");
            emailBody.Append("h1 { font-size: 28px; color: #333; }");
            emailBody.Append("p { font-size: 16px; color: #666; }");
            emailBody.Append("a { display: inline-block; padding: 10px 20px; margin-top: 15px; font-size: 18px; color: #fff; background-color: #007bff; text-decoration: none; border-radius: 5px; }");
            emailBody.Append("a:hover { background-color: #0056b3; }");
            emailBody.Append("</style>");
            emailBody.Append("</head>");
            emailBody.Append("<body>");
            emailBody.Append("<h1 class=\"display-8\">Forgot Password</h1>");
            emailBody.Append("<p>You are invited to reset your password. Click on the below link to reset your password:</p>");
            emailBody.Append("<a href='" + forgotPassword + "'>Reset Password</a>");
            emailBody.Append("</body>");
            emailBody.Append("</html>");
            return emailBody;
        }
    }
}