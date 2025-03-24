using AutoMapper;
using Domain.Entity.Product;

namespace Application.Features.CategoryFeatures.GetAllCategory;

public class GetAllCategoryMapper : Profile
{
    public GetAllCategoryMapper()
    {
        CreateMap<Category, GetAllCategoryResponse>();
    }
}