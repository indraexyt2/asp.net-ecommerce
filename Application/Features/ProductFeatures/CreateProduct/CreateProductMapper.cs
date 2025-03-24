using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using AutoMapper;
using Domain.Entity.Product;

namespace Application.Features.ProductFeatures.CreateProduct
{
    public sealed class CreateProductMapper : Profile
    {
        public CreateProductMapper()
        {
            CreateMap<Category, ProductCategoryResponse>();
            CreateMap<Domain.Entity.Product.Product, CreateProductResponse>()
                .ForMember(dest => dest.Category, 
                    opt => opt.MapFrom(src => src.Category));
        }
    }

}
