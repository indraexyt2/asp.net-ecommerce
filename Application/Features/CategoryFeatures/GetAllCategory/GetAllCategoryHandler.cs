using Application.Common.Exceptions;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.CategoryFeatures.GetAllCategory;

public class GetAllCategoryHandler(ICategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : 
    BaseHandlerCategory(repository, unitOfWork, mapper),
    IRequestHandler<GetAllCategoryRequest, List<GetAllCategoryResponse>>
{
    public async Task<List<GetAllCategoryResponse>> Handle(GetAllCategoryRequest request, CancellationToken cancellationToken)
    {
        var categories = await CategoryRepository.GetAll();
        if (categories.Count == 0)
        {
            throw new NullRequestException("Data tidak ditemukan");
        }

        return Mapper.Map<List<GetAllCategoryResponse>>(categories);
    }
}