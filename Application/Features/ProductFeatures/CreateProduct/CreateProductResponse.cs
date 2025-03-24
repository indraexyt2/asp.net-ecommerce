namespace Application.Features.ProductFeatures.CreateProduct
{
    public sealed record CreateProductResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public List<ProductCategoryResponse>? Category { get; set; }
    }

    public sealed record ProductCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
