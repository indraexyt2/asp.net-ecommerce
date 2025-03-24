using Application.Features.ProductFeatures.CreateProduct;

namespace Application.Features.ProductFeatures.UpdateProduct;

public sealed record UpdateProductResponse()
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public List<ProductCategoryResponse>? Category { get; set; }
}