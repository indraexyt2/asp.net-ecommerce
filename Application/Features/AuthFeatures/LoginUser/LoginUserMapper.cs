using AutoMapper;
using Domain.Entity.Identity;

namespace Application.Features.AuthFeatures.LoginUser;

public class LoginUserMapper : Profile
{
    public LoginUserMapper()
    {
         CreateMap<AppUser, LoginUserResponse>();
    }
}