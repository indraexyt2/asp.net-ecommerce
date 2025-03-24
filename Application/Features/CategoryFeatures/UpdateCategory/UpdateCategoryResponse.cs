namespace Application.Features.CategoryFeatures.UpdateCategory;

public sealed record UpdateCategoryResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}