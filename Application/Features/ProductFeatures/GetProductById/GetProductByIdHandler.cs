using Application.Common.Exceptions;
using Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProductFeatures.GetProductById;

public class GetProductByIdHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : 
    BaseHandlerProduct(productRepository, categoryRepository, unitOfWork, mapper), 
    IRequestHandler<GetProductByIdRequest, GetProductByIdResponse>
{
    public async Task<GetProductByIdResponse> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        var product = await ProductRepository.Get(request.Id, q => q.Include(p => p.Category));
        if (product == null)
        {
            throw new NullRequestException("Data tidak ditemukan");
        }

        return Mapper.Map<GetProductByIdResponse>(product);
    }
}