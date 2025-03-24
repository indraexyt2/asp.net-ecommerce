using System.Data;
using FluentValidation;

namespace Application.Features.ProductFeatures.GetProductById;

public sealed class GetProductByIdValidator : AbstractValidator<GetProductByIdRequest>
{
    public GetProductByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Product ID dibutuhkan");
    }
}