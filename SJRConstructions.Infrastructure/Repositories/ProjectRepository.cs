using SJRConstructions.Core.Entities;
using SJRConstructions.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJRConstructions.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<ProjectDetails>> GetAllProjects()
        {
            return await _dbContext.tbl_Project.ToListAsync();
        }
        public async Task<ProjectDetails> GetProjectsById(int projectId)
        {
            var project = await _dbContext.tbl_Project.FindAsync(projectId);
            return project;
        }
        public async Task<ProjectDetails> CreateLocationAsync(ProjectDetails projectDetails)
        {
            await _dbContext.AddAsync(projectDetails);
            await _dbContext.SaveChangesAsync();
            return projectDetails;
        }
        public async Task<ProjectDetails> UpdateLocationAsync(ProjectDetails projectDetails)
        {
            _dbContext.Attach(projectDetails);
            _dbContext.Entry(projectDetails).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return projectDetails;
        }

    }
}
