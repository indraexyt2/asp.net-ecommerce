using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using AutoMapper;
using Azure;
using Domain.Entity.Product;
using MediatR;

namespace Application.Features.ProductFeatures.CreateProduct
{
    public sealed class CreateProductHandler(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
        : BaseHandlerProduct(productRepository, categoryRepository, unitOfWork, mapper),
            IRequestHandler<CreateProductRequest, CreateProductResponse>
    {
        public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var listCategory = await CategoryRepository.GetAll();
            var categories = listCategory.Where(c => request.CategoryIds.Contains(c.Id)).ToList();

            var product = new Domain.Entity.Product.Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Category = categories
            };

            await ProductRepository.Add(product);
            await UnitOfWork.Save(cancellationToken);

            var productWithCategory = await ProductRepository.GetWithCategory(product.Id);
            if (productWithCategory is null)
                throw new Exception("Product not found");
            return Mapper.Map<CreateProductResponse>(productWithCategory);
        }
    }
}
