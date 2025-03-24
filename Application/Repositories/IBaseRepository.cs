using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Application.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        Task<T?> Get(Guid id, Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task<List<T>> GetAll(Func<IQueryable<T>, IQueryable<T>>? include = null);
    }
}
