using Application.Common.Exceptions;
using Application.Features.AuthFeatures.GetCurrentUser;
using Application.Features.AuthFeatures.LoginUser;
using Application.Features.AuthFeatures.RefreshToken;
using Application.Features.AuthFeatures.RegisterUser;
using Application.Features.AuthFeatures.RevokeToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterUserResponse>> Register([FromBody] RegisterUserRequest request, CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        return Ok(response);
    }   
    
    [HttpPost("login")]
    public async Task<ActionResult<LoginUserResponse>> Login([FromBody] LoginUserRequest request, CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        return Ok(response);
    }
    
    [HttpPost("refresh-token")]
    public async Task<ActionResult<RefreshTokenResponse>> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        return Ok(response);
    }
    
    [HttpPost("revoke-token")]
    [Authorize]
    public async Task<ActionResult<RevokeTokenResponse>> RevokeToken([FromBody] RevokeTokenRequest request, CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        if (!response.IsRevoked)
        {
            throw new InternalServerException("Gagal mencabut token");
        }
        return Ok(new { message = "Token berhasil dicabut" });
    }
    
    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<GetCurrentUserResponse>> GetCurrentUser(CancellationToken ct)
    {
        var response = await _mediator.Send(new GetCurrentUserRequest(), ct);
        return Ok(response);
    }
}