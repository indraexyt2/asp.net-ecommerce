using Application.Features.ProductFeatures.CreateProduct;
using Application.Features.ProductFeatures.GetProductById;
using AutoMapper;
using Domain.Entity.Product;

namespace Application.Features.ProductFeatures.GetAllProduct;

public class GetAllProductMapper : Profile
{
    public GetAllProductMapper()
    {
        CreateMap<Product, GetAllProductResponse>();;
    }
}