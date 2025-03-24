using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.ProductFeatures.CreateProduct
{
    public sealed record CreateProductRequest(string Name, string Description, decimal Price, List<Guid> CategoryIds) : IRequest<CreateProductResponse>
    {
    }
}
