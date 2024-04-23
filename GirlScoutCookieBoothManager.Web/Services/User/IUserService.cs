using GirlScoutCookieBoothManager.Core.Entities;

namespace GirlScoutCookieBoothManager.Web.Services.User
{
    public interface IUserService
    {
        UserProfile GetCurrentUser();
        Roles GetUserRoleDetails(int RoleId);
    }

}
