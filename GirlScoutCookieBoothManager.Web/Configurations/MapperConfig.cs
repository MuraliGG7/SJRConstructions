using AutoMapper;
using GirlScoutCookieBoothManager.Core.Entities;
using GirlScoutCookieBoothManager.Web.ViewModels;

namespace GirlScoutCookieBoothManager.Web.Configurations
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
