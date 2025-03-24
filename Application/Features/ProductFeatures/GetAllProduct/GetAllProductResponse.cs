using Application.Features.ProductFeatures.CreateProduct;

namespace Application.Features.ProductFeatures.GetAllProduct;

public sealed record GetAllProductResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public List<ProductCategoryResponse>? Category { get; set; }
}

// public sealed record GetAllProductResponse
// {
//     public List<GetAllProduct>? Products { get; set; }
// }