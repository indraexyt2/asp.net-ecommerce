using Application.Repositories;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T>(DbContext context) : IBaseRepository<T>
        where T : BaseEntity
    {
        protected readonly DbContext Context = context;

        public async Task<T> Add(T entity)
        {
            entity.DateCreated = DateTime.Now;
            entity.DateUpdated = DateTime.Now;
            await Context.AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public async Task<T?> Get(Guid id, Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = Context.Set<T>();
            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<T>> GetAll(Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = Context.Set<T>();
            if (include != null)
            {
                query = include(query);
            }
            
            return await query.ToListAsync();
        }

        public T Update(T entity)
        {
            Context.Set<T>().Update(entity);
            return entity;
        }
    }
}
