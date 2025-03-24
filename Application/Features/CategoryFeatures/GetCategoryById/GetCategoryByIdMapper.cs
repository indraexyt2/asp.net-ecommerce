using AutoMapper;
using Domain.Entity.Product;

namespace Application.Features.CategoryFeatures.GetCategoryById;

public sealed class GetCategoryByIdMapper : Profile
{
    public GetCategoryByIdMapper()
    {
        CreateMap<Category, GetCategoryByIdResponse>();
    }
}