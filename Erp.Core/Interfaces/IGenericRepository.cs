using Erp.Core.Pagination;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Erp.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdGuidAsync(Guid id);
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> GetAllWithOutFilter();        
        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);        
        Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        // Exposed so entity-specific repositories can build a custom
        // search predicate or apply Include()/Where() before paging.
        IQueryable<T> Query();

        Task<PagedResult<T>> GetAllItemsAsync(
            GridQueryParameters parameters,
            Expression<Func<T, bool>>? searchPredicate = null,
            IQueryable<T>? baseQuery = null);

    }
}
