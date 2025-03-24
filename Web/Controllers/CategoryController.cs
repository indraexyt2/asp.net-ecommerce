using Application.Common.Exceptions;
using Application.Features.CategoryFeatures.CreateCategory;
using Application.Features.CategoryFeatures.DeleteCategory;
using Application.Features.CategoryFeatures.GetAllCategory;
using Application.Features.CategoryFeatures.GetCategoryById;
using Application.Features.CategoryFeatures.UpdateCategory;
using Application.Features.ProductFeatures.DeletProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateCategoryResponse>> Add(CreateCategoryRequest request, CancellationToken ct)
        {
            var response = await _mediator.Send(request, ct);
            return Ok(response);
        }
        
        [HttpGet]
        public async Task<ActionResult<List<GetAllCategoryResponse>>> GetAll(CancellationToken ct)
        {
            var response = await _mediator.Send(new GetAllCategoryRequest(), ct);
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetCategoryByIdResponse>> Get(Guid id, CancellationToken ct)
        {
            var response = await _mediator.Send(new GetCategoryByIdRequest(id), ct);
            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UpdateCategoryResponse>> Update(Guid id, [FromBody] UpdateCategoryDto request, CancellationToken ct)
        {
            var response = await _mediator.Send(new UpdateCategoryRequest(id, request.Name), ct);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<DeleteProductResponse>> Delete(Guid id, CancellationToken ct)
        {
            var response = await _mediator.Send(new DeleteCategoryRequest(id), ct);
            if (!response.IsDeleted) throw new InternalServerException("Terjadi kesalahan pada server");
            return NoContent();
        }
    }
}
