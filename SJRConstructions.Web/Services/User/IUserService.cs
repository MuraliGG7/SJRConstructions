using SJRConstructions.Core.Entities;

namespace SJRConstructions.Web.Services.User
{
    public interface IUserService
    {
        UserProfile GetCurrentUser();
        Roles GetUserRoleDetails(int RoleId);
    }

}
