using FluentValidation;

namespace Application.Features.AuthFeatures.LoginUser;

public class LoginUserValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username tidak boleh kosong");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password tidak boleh kosong");
    }
}