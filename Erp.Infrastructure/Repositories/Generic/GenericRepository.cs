using Erp.Core;
using Erp.Core.Interfaces;
using Erp.Core.Pagination;
using Erp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Erp.Infrastructure.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync();
        }

        public IQueryable<T> GetAllWithOutFilter()
        {
            return _dbSet;
        }
        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }



        public async Task<T?> GetByIdGuidAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
            }

        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
        }

        /////////////////////////////////////////////////////
        ///
        public IQueryable<T> Query() => _dbSet.AsQueryable();

        public async Task<PagedResult<T>> GetAllItemsAsync(
            GridQueryParameters parameters,
            Expression<Func<T, bool>>? searchPredicate = null,
            IQueryable<T>? baseQuery = null)
        {
            // baseQuery lets a specific repository pass Includes/joins in;
            // otherwise we just start from the table.
            IQueryable<T> query = baseQuery ?? _dbSet.AsNoTracking();

            // 1) SEARCH — filters before we count, so paging math is correct
            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm) && searchPredicate != null)
            {
                query = query.Where(searchPredicate);
            }

            var totalCount = await query.CountAsync();

            // 2) SORT — dynamic column name -> "Name asc" / "Email desc"
            //    (System.Linq.Dynamic.Core translates this to SQL ORDER BY)
            if (!string.IsNullOrWhiteSpace(parameters.SortColumn))
            {
                var direction = string.Equals(parameters.SortDirection, "desc", StringComparison.OrdinalIgnoreCase)
                    ? "descending"
                    : "ascending";

                // Guard against garbage column names blowing up with a 500.
                var validProperty = typeof(T).GetProperty(parameters.SortColumn,
                    System.Reflection.BindingFlags.IgnoreCase |
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.Instance);

                if (validProperty != null)
                {
                    //query = query.OrderBy($"{validProperty.Name} {direction}");
                }
            }

            // 3) PAGE — always LAST, and always via Skip/Take so SQL Server
            //    only ever pulls back one page of rows.
            var items = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                SearchTerm = parameters.SearchTerm,
                SortColumn = parameters.SortColumn,
                SortDirection = parameters.SortDirection
            };
        }

    }
}
