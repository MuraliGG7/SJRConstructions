using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GirlScoutCookieBoothManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GirlScoutCookieBoothManager.Infrastructure.Configurations.Entities
{
    public class RoleSeedConfiguration : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.HasData(
                    new Roles
                    {
                        RoleID =1,
                        RoleName= "Admin"
                    },
                    new Roles
                    {
                        RoleID = 2,
                        RoleName = "Manager"
                    },
                    new Roles
                    {
                        RoleID = 3,
                        RoleName = "Troop Coordinator"
                    }
                );
        }
    }
}
