using AutoMapper;
using Domain.Entity.Identity;

namespace Application.Features.AuthFeatures.RegisterUser;

public class RegisterUserMapper : Profile
{
    public RegisterUserMapper()
    {
        CreateMap<AppUser, RegisterUserResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Roles, opt => opt.Ignore());
    }
}