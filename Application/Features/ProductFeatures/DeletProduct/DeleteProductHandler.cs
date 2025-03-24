using Application.Common.Exceptions;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.ProductFeatures.DeletProduct;

public class DeleteProductHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : 
    BaseHandlerProduct(productRepository, categoryRepository, unitOfWork, mapper), IRequestHandler<DeleteProductRequest, DeleteProductResponse>
{
    public async Task<DeleteProductResponse> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var product = await ProductRepository.GetWithCategory(request.ProductId);
        if (product == null)
        {
            throw new NullRequestException("Data produk tidak ditemukan");
        }
        
        ProductRepository.RemoveCategories(request.ProductId);
        ProductRepository.Delete(product);

        var affectedRow = await UnitOfWork.Save(cancellationToken);
        if (affectedRow <= 0)
        {
            throw new InternalServerException("Terjadi kesalahan pada server");
        }

        return new DeleteProductResponse(true);
    }
}