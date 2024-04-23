using GirlScoutCookieBoothManager.Core.Entities;
using GirlScoutCookieBoothManager.Core.Interfaces;
using System.Security.Claims;

namespace GirlScoutCookieBoothManager.Web.Services.User
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public UserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public UserProfile GetCurrentUser()
        {
            var UserID = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (UserID != null)
            {
               return _userRepository.GetUserById(UserID);
            }
            else
            {
                return null;
            }
        }
        public Roles GetUserRoleDetails(int RoleId)
        {
            return _userRepository.GetRolesById(RoleId);
        }
    }
}
