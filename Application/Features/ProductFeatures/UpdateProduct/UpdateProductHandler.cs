using Application.Common.Exceptions;
using Application.Repositories;
using AutoMapper;
using Domain.Entity.Product;
using MediatR;

namespace Application.Features.ProductFeatures.UpdateProduct;

public class UpdateProductHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : 
    BaseHandlerProduct(productRepository, categoryRepository, unitOfWork, mapper), IRequestHandler<UpdateProductRequest, UpdateProductResponse>
{
    public async Task<UpdateProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
{
    var existingProduct = await ProductRepository.Get(request.Id);
    if (existingProduct == null)
    {
        throw new NullRequestException("Data tidak ditemukan");
    }
    
    ProductRepository.RemoveCategories(existingProduct.Id);
    await UnitOfWork.Save(cancellationToken);
    
    var categoriesToAdd = await CategoryRepository.GetCategoriesById(request.CategoryIds);
    foreach (var newCategory in categoriesToAdd)
    {
        existingProduct.Category.Add(newCategory);
    }
    
    existingProduct.Name = request.Name ?? existingProduct.Name;
    existingProduct.Description = request.Description ?? existingProduct.Description;
    existingProduct.Price = request.Price;

    await UnitOfWork.Save(cancellationToken);
    
    return Mapper.Map<UpdateProductResponse>(existingProduct);
}
}