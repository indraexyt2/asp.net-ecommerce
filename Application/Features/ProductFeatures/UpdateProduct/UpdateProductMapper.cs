using Application.Features.ProductFeatures.CreateProduct;
using AutoMapper;
using Domain.Entity.Product;

namespace Application.Features.ProductFeatures.UpdateProduct;

public class UpdateProductMapper : Profile
{
    public UpdateProductMapper()
    {
        CreateMap<Category, ProductCategoryResponse>();
        CreateMap<UpdateProductRequest, Product>();
        CreateMap<Product, UpdateProductResponse>()
            .ForMember(dest => dest.Category, 
                opt => opt.MapFrom(src => src.Category));
    }
}