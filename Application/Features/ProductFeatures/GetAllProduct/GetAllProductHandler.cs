using Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProductFeatures.GetAllProduct;

public class GetAllProductHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : 
    BaseHandlerProduct(productRepository, categoryRepository, unitOfWork, mapper),
    IRequestHandler<GetAllProductRequest, List<GetAllProductResponse>>
{
    public async Task<List<GetAllProductResponse>> Handle(GetAllProductRequest request, CancellationToken cancellationToken)
    {
        var products = await ProductRepository.GetAll(q => q.Include(p => p.Category));
        return Mapper.Map<List<GetAllProductResponse>>(products);
    }
}