using MediatR;

namespace Application.Features.ProductFeatures.GetProductById;

public sealed record GetProductByIdRequest(Guid Id) : IRequest<GetProductByIdResponse>
{
}