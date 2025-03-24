using MediatR;

namespace Application.Features.CategoryFeatures.DeleteCategory;

public sealed record DeleteCategoryRequest(Guid Id) : IRequest<DeleteCategoryResponse>;