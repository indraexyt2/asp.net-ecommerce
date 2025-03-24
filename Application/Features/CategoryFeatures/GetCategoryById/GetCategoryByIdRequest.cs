using MediatR;

namespace Application.Features.CategoryFeatures.GetCategoryById;

public sealed record GetCategoryByIdRequest(Guid Id) : IRequest<GetCategoryByIdResponse>;