using MediatR;

namespace Application.Features.ProductFeatures.UpdateProduct;

public sealed record UpdateProductRequest(Guid Id, string Name, string Description, decimal Price, List<Guid> CategoryIds) : IRequest<UpdateProductResponse>;

public sealed record UpdateProductDto(string Name, string Description, decimal Price, List<Guid> CategoryIds);