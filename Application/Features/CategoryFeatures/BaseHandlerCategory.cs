using Application.Repositories;
using AutoMapper;

namespace Application.Features.CategoryFeatures;

public class BaseHandlerCategory(
    ICategoryRepository repository, 
    IUnitOfWork unitOfWork, 
    IMapper mapper)
{
    protected readonly ICategoryRepository CategoryRepository = repository;
    protected readonly IUnitOfWork UnitOfWork = unitOfWork;
    protected readonly IMapper Mapper = mapper;
}