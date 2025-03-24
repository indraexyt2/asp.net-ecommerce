using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Entity.Product;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {
        }

        public async Task<Product?> GetWithCategory(Guid id)
        {
            return await Context.Set<Product>()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void RemoveCategories(Guid productId)
        {
            var product = Context.Set<Product>()
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == productId);

            if (product == null) return;
            product.Category.Clear(); 
            Context.SaveChanges();
        }
    }
}
