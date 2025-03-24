using Application.Common.Exceptions;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.CategoryFeatures.GetCategoryById;

public class GetCategoryByIdHandler(ICategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : 
    BaseHandlerCategory(repository, unitOfWork, mapper),
    IRequestHandler<GetCategoryByIdRequest, GetCategoryByIdResponse>
{
    public async Task<GetCategoryByIdResponse> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
    {
        var category = await CategoryRepository.Get(request.Id);
        if (category == null)
        {
            throw new NullRequestException("Data tidak ditemukan");
        }

        return Mapper.Map<GetCategoryByIdResponse>(category);
    }
}