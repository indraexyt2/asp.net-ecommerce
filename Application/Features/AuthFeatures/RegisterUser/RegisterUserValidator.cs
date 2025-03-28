using Domain.Entity.Identity;
using FluentValidation;

namespace Application.Features.AuthFeatures.RegisterUser;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username tidak boleh kosong")
            .MinimumLength(3).WithMessage("Username minimal 3 karakter")
            .MaximumLength(50).WithMessage("Username maksimal 50 karakter");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email tidak boleh kosong")
            .EmailAddress().WithMessage("Format email tidak valid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password tidak boleh kosong")
            .MinimumLength(6).WithMessage("Password minimal 6 karakter")
            .Matches("[A-Z]").WithMessage("Password harus mengandung huruf kapital")
            .Matches("[a-z]").WithMessage("Password harus mengandung huruf kecil")
            .Matches("[0-9]").WithMessage("Password harus mengandung angka")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password harus mengandung karakter spesial");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Nama depan tidak boleh kosong")
            .MaximumLength(50).WithMessage("Nama depan maksimal 50 karakter");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Nama belakang tidak boleh kosong")
            .MaximumLength(50).WithMessage("Nama belakang maksimal 50 karakter");

        RuleFor(x => x.Role)
            .Must(role => Roles.All.Contains(role))
            .WithMessage($"Role harus salah satu dari: {string.Join(", ", Roles.All)}");
    }
}