using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Product;
using FluentValidation;

namespace Application.Features.CategoryFeatures.CreateCategory
{
    public sealed class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nama kategori tidak boleh kosong");
        }
    }
}
