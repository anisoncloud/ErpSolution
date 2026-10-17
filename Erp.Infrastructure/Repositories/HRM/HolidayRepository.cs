using Microsoft.EntityFrameworkCore;
using Erp.Infrastructure.Data;
using Erp.Infrastructure.Repositories.Generic;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Repositories.HRM
{
    public class HolidayRepository : GenericRepository<Holiday>, IHolidayRepository
    {
        public HolidayRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<HashSet<DateOnly>> GetDatesAsync(DateOnly from, DateOnly to)
        {
            return (await _dbSet
            .Where(h => !h.IsDeleted && h.Date >= from && h.Date <= to)
            .Select(h => h.Date)
            .ToListAsync()).ToHashSet();
        }
        

        public async Task<List<Holiday>> GetByYearAsync(int year)
        {
            var start = new DateOnly(year, 1, 1);
            var end = new DateOnly(year, 12, 31);
            return await _dbSet
                .Where(h => !h.IsDeleted && h.Date >= start && h.Date <= end)
                .OrderBy(h => h.Date)
                .ToListAsync();
        }

        // named differently from the generic GetByIdAsync so soft-deleted rows are never returned
        public Task<Holiday?> GetActiveByIdAsync(int id) =>
            _dbSet.FirstOrDefaultAsync(h => h.Id == id && !h.IsDeleted);

        public Task<bool> DateExistsAsync(DateOnly date, int? excludeId = null) =>
            _dbSet.AnyAsync(h => !h.IsDeleted && h.Date == date && (excludeId == null || h.Id != excludeId));
    }
}
