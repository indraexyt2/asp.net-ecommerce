namespace Application.Features.CategoryFeatures.GetAllCategory;

public sealed record GetAllCategoryResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}