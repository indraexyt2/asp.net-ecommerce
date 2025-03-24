using FluentValidation;

namespace Application.Features.ProductFeatures.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nama product tidak boleh kosong")
            .NotNull().WithMessage("Nama product tidak boleh kosong")
            .MinimumLength(5).WithMessage("Minimal 5 karakter");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Harga tidak boleh kosong")
            .NotNull().WithMessage("Nama product tidak boleh kosong")
            .GreaterThanOrEqualTo(10000).WithMessage("Harga tidak boleh dibawah Rp10.000");

        RuleFor(x => x.CategoryIds)
            .NotEmpty().WithMessage("Minimal ada 1 kategori")
            .NotNull().WithMessage("Nama product tidak boleh kosong");
    }
}