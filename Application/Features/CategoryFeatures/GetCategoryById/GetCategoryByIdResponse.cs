namespace Application.Features.CategoryFeatures.GetCategoryById;

public sealed record GetCategoryByIdResponse
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
}