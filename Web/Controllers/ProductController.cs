using Application.Common.Exceptions;
using Application.Features.ProductFeatures.CreateProduct;
using Application.Features.ProductFeatures.DeletProduct;
using Application.Features.ProductFeatures.GetAllProduct;
using Application.Features.ProductFeatures.GetProductById;
using Application.Features.ProductFeatures.UpdateProduct;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateProductResponse>> Add(CreateProductRequest? request, CancellationToken ct)
        {
            if (request == null)
            {
                throw new BadRequestException("Request tidak valid", "Request body tidak boleh kosong");
            }
            
            var response = await _mediator.Send(request, ct);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllProductResponse>>> GetAll(CancellationToken ct)
        {
            var response = await _mediator.Send(new GetAllProductRequest(), ct);
            return Ok(response);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetProductByIdResponse>> Get(Guid id, CancellationToken ct)
        {
            var request = new GetProductByIdRequest(id);
            var response = await _mediator.Send(request, ct);

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UpdateProductResponse>> Update(Guid id, [FromBody] UpdateProductDto request,
            CancellationToken ct)
        {
            if (id == Guid.Empty)
            {
                throw new BadRequestException("Request tidak valid", "Produk id tidak boleh kosong");
            }
            
            var command = new UpdateProductRequest(id, request.Name, request.Description, request.Price, request.CategoryIds);
            var response = await _mediator.Send(command, ct);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<DeleteProductResponse>> Delete([FromRoute] Guid id, CancellationToken ct)
        {
            var request = new DeleteProductRequest(id);
            var response = await _mediator.Send(request, ct);
            if (response.IsDeleted == false)
            {
                throw new InternalServerException("Terjadi kesalahan pada server haha");
            }

            return NoContent();
        }
    }
}
