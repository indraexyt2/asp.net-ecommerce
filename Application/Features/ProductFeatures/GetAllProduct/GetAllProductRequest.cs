using MediatR;

namespace Application.Features.ProductFeatures.GetAllProduct;

public sealed record GetAllProductRequest() : IRequest<List<GetAllProductResponse>>;