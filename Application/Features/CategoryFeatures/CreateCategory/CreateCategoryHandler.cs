using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using AutoMapper;
using Domain.Entity.Product;
using MediatR;

namespace Application.Features.CategoryFeatures.CreateCategory
{
    public sealed class CreateCategoryHandler(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
        : BaseHandlerCategory(categoryRepository, unitOfWork, mapper),
            IRequestHandler<CreateCategoryRequest, CreateCategoryResponse>
    {
        public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = Mapper.Map<Category>(request);
            await CategoryRepository.Add(category);
            await UnitOfWork.Save(cancellationToken);

            return Mapper.Map<CreateCategoryResponse>(category);
        }
    }
}
