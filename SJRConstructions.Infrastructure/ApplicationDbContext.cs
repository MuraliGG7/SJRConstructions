using SJRConstructions.Core.Entities;
using SJRConstructions.Infrastructure.Configurations.Entities;
using Microsoft.EntityFrameworkCore;

namespace SJRConstructions.Infrastructure // Replace with your namespace
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Define DbSet properties for your entities here
        //Example:
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserProfile> Users { get; set; }
        public DbSet<EmailService> EmailServices { get; set; }    
        public DbSet<ProjectDetails> tbl_Project { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<TroopDetails>().HasOne(u => u.user).WithMany().HasForeignKey(u => u.UserID);
            //modelBuilder.Entity<GenerateLottery>().HasOne(u => u.Location).WithMany().HasForeignKey(u => u.LocationId);
            modelBuilder.ApplyConfiguration(new RoleSeedConfiguration());
            modelBuilder.ApplyConfiguration(new UserSeedConfiguration());
        }        
    }
}