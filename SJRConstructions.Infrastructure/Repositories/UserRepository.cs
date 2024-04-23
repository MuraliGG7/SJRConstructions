using SJRConstructions.Core.Entities;
using SJRConstructions.Core.Interfaces;

namespace SJRConstructions.Infrastructure.Repositories
{
    // UserRepository.cs
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserProfile GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }
        public List<UserProfile> GetTroopCoordinatorsUsers(int RoleID)
        {
            return _dbContext.Users.Where(u => u.RoleID == RoleID).ToList();
        }
        public bool IsEmailInUse(string email)
        {
            // Check if the email exists in the database
            return _dbContext.Users.Any(u => u.Email == email);
        }

        public bool IsEmailInUse(string email,string UserID)
        {
            // Check if the email exists in the database
            return _dbContext.Users.Any(u => u.Email == email && u.UserID.ToString() == UserID);
        }

        public UserProfile GetUserById(string userID)
        {
            return _dbContext.Users.FirstOrDefault(u => u.UserID.ToString() == userID);
        }
        public int Register(UserProfile user)
        {
            // Logic to save the user in the database
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user.UserID;
        }
        public void Update(UserProfile user)
        {
            // Logic to save the user in the database
            _dbContext.Users.Update(user);           
            _dbContext.SaveChanges();
        }
        public void UpdatePassword(UserProfile user)
        {
            // Logic to save the user in the database
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }
        public Roles GetRolesById(int RoleID)
        {
            return _dbContext.Roles.FirstOrDefault(u => u.RoleID == RoleID);
        }
    }

}
