using Application.Common.Exceptions;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.CategoryFeatures.DeleteCategory;

public class DeleteCategoryHandler(ICategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : 
    BaseHandlerCategory(repository, unitOfWork, mapper),
    IRequestHandler<DeleteCategoryRequest, DeleteCategoryResponse>
{
    public async Task<DeleteCategoryResponse> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await CategoryRepository.Get(request.Id);
        if (category == null)
        {
            throw new NullRequestException("Data tidak ditemukan");
        }
        
        CategoryRepository.Delete(category);
        var affectedRow = await UnitOfWork.Save(cancellationToken);
        if (affectedRow <= 0)
        {
            throw new InternalServerException("Terjadi kesalahan pada server");
        }

        return new DeleteCategoryResponse(true);
    }
}