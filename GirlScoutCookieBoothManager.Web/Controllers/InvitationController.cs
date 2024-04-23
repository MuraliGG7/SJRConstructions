using System.Text;
using AutoMapper;
using GirlScoutCookieBoothManager.Core.Entities;
using GirlScoutCookieBoothManager.Core.Interfaces;
using GirlScoutCookieBoothManager.Web.Enums;
using GirlScoutCookieBoothManager.Web.Services.EmailService;
using GirlScoutCookieBoothManager.Web.Services.User;
using GirlScoutCookieBoothManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GirlScoutCookieBoothManager.Web.Controllers
{
    public class InvitationController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IMapper mapper;
        private readonly IEmailRepository emailRepository;
        private readonly IConfiguration configuration;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private UserProfile UserData;

        public InvitationController(IEmailSender emailSender, IMapper mapper, IEmailRepository emailRepository, IConfiguration configuration,
            IUserRepository userRepository,IUserService userService) {
            this._emailSender = emailSender;
            this.mapper = mapper;
            this.emailRepository = emailRepository;
            this.configuration = configuration;
            _userRepository = userRepository;
            _userService = userService;
            UserData = _userService.GetCurrentUser();
        }
        public IActionResult Index()
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Invite(InvitationVM invitationVM)
       {
            if(ModelState.IsValid)
            {

                invitationVM.InviteToken = Guid.NewGuid().ToString();
                invitationVM.Type = "RegistrationInvite";
                var invitation = mapper.Map<EmailService>(invitationVM);
                await emailRepository.SaveInvitationAsync(invitation);


                string invitationEmail = invitation.Email;           

                if(!string.IsNullOrWhiteSpace(invitationEmail))
                { 

                    StringBuilder registerEmailBody = GetEmailContent(invitationVM.InviteToken);           
                                   
                    var message = new Message(new string[] { invitationEmail }, "Register for Girl Scout Cookie Booth", registerEmailBody.ToString(), null);
                    await _emailSender.SendEmailAsync(message);                    
                    TempData["successMessage"] = "Registration invitation sent successfully";
                }
            }
            return RedirectToAction(nameof(Index));            
        }
        private StringBuilder GetEmailContent(string token)
        {
            string registerLink = configuration.GetValue<string>("UserInformation:InvitationLink") + token;
            TempData["registrationLink"] = registerLink;
            StringBuilder emailBody = new StringBuilder();
            emailBody.Append("<h2>Registration Invitation</h2>");
            emailBody.Append("<p>You are invited to register for Girl Scout Cookie Booth. Click on the below link to register.</p>");
            emailBody.Append("<a href='" + registerLink + "'>Register Cookie Booth</a>");
            emailBody.Append("<br/><br/>");
            emailBody.Append("Thank you,<br/>");
            emailBody.Append("Girl Scout Cookie Booth Team");
            return emailBody;
        }
    }
}
