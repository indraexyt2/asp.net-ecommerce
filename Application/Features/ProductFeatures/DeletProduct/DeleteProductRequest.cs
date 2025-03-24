using MediatR;

namespace Application.Features.ProductFeatures.DeletProduct;

public sealed record DeleteProductRequest(Guid ProductId) : IRequest<DeleteProductResponse>
{
    
}