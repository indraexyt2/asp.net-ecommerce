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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Category>> GetCategoriesById(List<Guid> ids)
        {
            return await Context.Set<Category>()
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();
        }
    }
}
