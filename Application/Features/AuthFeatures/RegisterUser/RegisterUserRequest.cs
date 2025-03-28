using MediatR;

namespace Application.Features.AuthFeatures.RegisterUser;

public sealed record RegisterUserRequest(
    string Username, 
    string Email, 
    string Password, 
    string FirstName, 
    string LastName, 
    string Role = "Client") : IRequest<RegisterUserResponse>;