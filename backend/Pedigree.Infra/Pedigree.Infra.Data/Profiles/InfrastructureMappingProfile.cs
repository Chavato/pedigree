using AutoMapper;
using Pedigree.Application.Models.DTOs;
using Pedigree.Infra.Data.Identity;

namespace Pedigree.Infra.Data.Profiles
{
    public class InfrastructureMappingProfile : Profile
    {
        public InfrastructureMappingProfile()
        {
            CreateMap<ApplicationUserDTO, ApplicationUser>().ReverseMap();
        }
    }
}