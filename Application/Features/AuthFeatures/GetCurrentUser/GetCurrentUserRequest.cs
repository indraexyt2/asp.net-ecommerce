using MediatR;

namespace Application.Features.AuthFeatures.GetCurrentUser;

public sealed record GetCurrentUserRequest() : IRequest<GetCurrentUserResponse>;