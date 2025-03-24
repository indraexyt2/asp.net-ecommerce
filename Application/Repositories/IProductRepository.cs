using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Product;

namespace Application.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product?> GetWithCategory(Guid productId);
        void RemoveCategories(Guid productId);
    }
}
