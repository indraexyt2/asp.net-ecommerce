using MediatR;

namespace Application.Features.AuthFeatures.LoginUser;

public sealed record LoginUserRequest(string Username, string Password) : IRequest<LoginUserResponse>;