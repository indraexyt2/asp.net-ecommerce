using MediatR;

namespace Application.Features.CategoryFeatures.UpdateCategory;

public sealed record UpdateCategoryRequest(Guid Id, string Name) : IRequest<UpdateCategoryResponse>;

public sealed record UpdateCategoryDto(string Name);