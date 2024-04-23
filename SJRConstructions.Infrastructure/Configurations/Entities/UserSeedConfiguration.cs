using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SJRConstructions.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SJRConstructions.Infrastructure.Configurations.Entities
{
    public class UserSeedConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            var passwordHasher = new PasswordHasher<UserProfile>();
            builder.HasData(
                new UserProfile
                {
                    UserID = 1,
                    RoleID =1,
                    Email = "cookieadmin@gmail.com",
                    UserName = "Admin",
                    Password = passwordHasher.HashPassword(null, "Admin@123"),
                    CreatedBy="Admin"
                },
                new UserProfile
                {
                    UserID = 2,
                    RoleID = 2,
                    Email = "cookiemanager@gmail.com",
                    UserName = "Manager",
                    Password = passwordHasher.HashPassword(null, "Manager@123"),
                    CreatedBy = "Admin"
                }
                );
        }
    }
}
