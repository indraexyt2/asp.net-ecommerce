using Application.Features.CategoryFeatures.CreateCategory;
using Application.Features.CategoryFeatures.GetCategoryById;
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

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetCategoryByIdResponse>> Get(Guid id, CancellationToken ct)
        {
            var response = await _mediator.Send(new GetCategoryByIdRequest(id), ct);
            return Ok(response);
        }
    }
}
