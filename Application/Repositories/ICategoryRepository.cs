using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Product;

namespace Application.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<List<Category>> GetCategoriesById(List<Guid> id);
    }
}
