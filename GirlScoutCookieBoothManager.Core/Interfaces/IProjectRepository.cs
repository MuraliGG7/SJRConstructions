using GirlScoutCookieBoothManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GirlScoutCookieBoothManager.Core.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<ProjectDetails>> GetAllProjects();
        Task<ProjectDetails> CreateLocationAsync(ProjectDetails projectDetails);
        Task<ProjectDetails> GetProjectsById(int projectId);
        Task<ProjectDetails> UpdateLocationAsync(ProjectDetails projectDetails);
    }
}
