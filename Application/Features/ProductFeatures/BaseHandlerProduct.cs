using Application.Repositories;
using AutoMapper;

namespace Application.Features.ProductFeatures;

public class BaseHandlerProduct(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
{
    protected readonly IProductRepository ProductRepository = productRepository;
    protected readonly ICategoryRepository CategoryRepository = categoryRepository;
    protected readonly IUnitOfWork UnitOfWork = unitOfWork;
    protected readonly IMapper Mapper = mapper;
}