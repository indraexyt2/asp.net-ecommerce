using MediatR;

namespace Application.Features.CategoryFeatures.GetAllCategory;

public sealed record GetAllCategoryRequest() : IRequest<List<GetAllCategoryResponse>>;