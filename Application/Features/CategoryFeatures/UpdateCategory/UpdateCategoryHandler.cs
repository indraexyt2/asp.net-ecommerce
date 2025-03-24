using Application.Common.Exceptions;
using Application.Features.ProductFeatures.UpdateProduct;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.CategoryFeatures.UpdateCategory;

public class UpdateCategoryHandler(ICategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : 
    BaseHandlerCategory(repository, unitOfWork, mapper),
    IRequestHandler<UpdateCategoryRequest, UpdateCategoryResponse>
{
    public async Task<UpdateCategoryResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await CategoryRepository.Get(request.Id);
        if (category == null)
        {
            throw new NullRequestException("Data tidak ditemukan");
        }

        category.Name = request.Name;
        await UnitOfWork.Save(cancellationToken);

        return Mapper.Map<UpdateCategoryResponse>(category);
    }
}