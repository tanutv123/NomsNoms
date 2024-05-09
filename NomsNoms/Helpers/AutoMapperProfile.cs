using AutoMapper;
using NomsNoms.DTOs;
using NomsNoms.Entities;

namespace NomsNoms.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<AppUser, UserDTO>().ReverseMap();
            CreateMap<AppUser, UserAdminDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRoles.First().AppRole.Name))
                .ReverseMap();
            CreateMap<AppUser, UserProfileDTO>()
                .ReverseMap();
        }
    }
}
