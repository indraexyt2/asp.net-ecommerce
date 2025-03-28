using Application.Repositories;
using Application.Services;
using AutoMapper;

namespace Application.Features.AuthFeatures;

public class BaseHandlerAuth
{
    protected readonly IUserRepository UserRepository;
    protected readonly IAuthService AuthService;
    protected readonly IMapper Mapper;

    public BaseHandlerAuth(IUserRepository userRepository, IAuthService authService, IMapper mapper)
    {
        UserRepository = userRepository;
        AuthService = authService;
        Mapper = mapper;
    }
}