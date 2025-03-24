using AutoMapper;
using Domain.Entity.Product;

namespace Application.Features.CategoryFeatures.UpdateCategory;

public class UpdateCategoryMapper : Profile
{
    public UpdateCategoryMapper()
    {
        CreateMap<Category, UpdateCategoryResponse>();
    }
}