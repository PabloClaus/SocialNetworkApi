using AutoMapper;
using SocialNetworkApi.Services;

namespace SocialNetworkApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Common.DTO.POST.Registration.ApplicationUser, Core.Entities.ApplicationUser>()
                .ForMember(dest => dest.PasswordHash,
                    opt => opt.MapFrom(scr => BCrypt.Net.BCrypt.HashPassword(scr.Password)))
                .ForMember(dest => dest.RoleId,
                    opt => opt.MapFrom(scr => ApplicationUserService.ApplicationRole.User))
                .ForMember(dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Role,
                opt => opt.Ignore());

            CreateMap<Core.Entities.ApplicationUser, Common.DTO.GET.GetUsers.ApplicationUser>();

            CreateMap<Core.Entities.ApplicationUser, Common.DTO.GET.GetUser.UserIDs>();

            CreateMap<Core.Entities.ApplicationUser, Common.DTO.POST.Authentication.AuthenticationResponse>()
                .ForMember(dest => dest.Token,
                    opt => opt.Ignore());


        }
    }
}
