using AutoMapper;
using SJRConstructions.Core.Entities;
using SJRConstructions.Web.ViewModels;

namespace SJRConstructions.Web.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<EmailService, InvitationVM>().ReverseMap();
            CreateMap<ProjectDetails, ProjectDetailsVM>().ReverseMap();
            

        }
    }
}
