using Erp.Modules.HRM.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Repositories
{
    public interface IHolidayRepository
    {
        Task<HashSet<DateOnly>> GetDatesAsync(DateOnly from, DateOnly to);
        Task<List<Holiday>> GetByYearAsync(int year);
        Task<Holiday?> GetByIdAsync(int id);
        Task<bool> DateExistsAsync(DateOnly date, int? excludeId = null);
        Task AddAsync(Holiday holiday);
    }
}
