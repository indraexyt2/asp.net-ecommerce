using Application.Features.ProductFeatures.CreateProduct;
using AutoMapper;
using Domain.Entity.Product;

namespace Application.Features.ProductFeatures.GetProductById;

public sealed class GetProductByIdMapper : Profile
{
    public GetProductByIdMapper()
    {
        CreateMap<Product, GetProductByIdResponse>()
            .ForMember(dst => dst.Category, 
                opt => opt.MapFrom(src => src.Category));
    }
}