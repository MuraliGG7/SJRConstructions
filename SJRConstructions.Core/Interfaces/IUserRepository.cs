using SJRConstructions.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJRConstructions.Core.Interfaces
{

    public interface IUserRepository
    {
        UserProfile GetUserByEmail(string email);
        UserProfile GetUserById(string userid);        
        bool IsEmailInUse(string email);
        bool IsEmailInUse(string email, string UserID);
        int Register(UserProfile user);        
        void Update(UserProfile user);
        void UpdatePassword(UserProfile user);
        Roles GetRolesById(int RoleID);
        List<UserProfile> GetTroopCoordinatorsUsers(int RoleID);
    }
}
