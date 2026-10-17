using Erp.Infrastructure.Data;
using Erp.Infrastructure.Repositories.Generic;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Enums;
using Erp.Modules.HRM.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Repositories.HRM
{
    public class LeaveApplicationRepository : GenericRepository<LeaveApplication>, ILeaveApplicationRepository
    {
        public LeaveApplicationRepository(AppDbContext context) : base(context)
        {
        }

        public Task<bool> HasOverlapAsync(int employeeId, DateOnly from, DateOnly lastDay) =>
        // existing last day = ReturnDate - 1, so "existing.last >= from" is "existing.ReturnDate > from"
        _dbSet.AnyAsync(a =>
            a.EmployeeId == employeeId
            && !a.IsDeleted
            && (a.Status == LeaveStatus.Pending || a.Status == LeaveStatus.Approved)
            && a.FromDate <= lastDay
            && a.ReturnDate > from);

        public async Task<List<UsageRow>> GetUsageAsync(int employeeId, int year)
        {
            var rows = await _context.Set<LeaveApplicationYear>()
                .Where(y => y.Year == year
                            && y.Application!.EmployeeId == employeeId
                            && !y.Application.IsDeleted
                            && (y.Application.Status == LeaveStatus.Approved || y.Application.Status == LeaveStatus.Pending))
                .GroupBy(y => new { y.Application!.LeaveType, y.Application.Status })
                .Select(g => new { g.Key.LeaveType, g.Key.Status, Days = g.Sum(y => y.Days), Cf = g.Sum(y => y.CarryForwardDays) })
                .ToListAsync();

            return rows.GroupBy(r => r.LeaveType)
                .Select(g => new UsageRow(
                    g.Key,
                    g.Where(r => r.Status == LeaveStatus.Approved).Sum(r => r.Days),
                    g.Where(r => r.Status == LeaveStatus.Pending).Sum(r => r.Days),
                    g.Where(r => r.Status == LeaveStatus.Approved).Sum(r => r.Cf),
                    g.Where(r => r.Status == LeaveStatus.Pending).Sum(r => r.Cf)))
                .ToList();
        }

        public async Task<LifetimeUsage> GetLifetimeUsageAsync(int employeeId, LeaveType type)
        {
            var rows = await _dbSet
                .Where(a => a.EmployeeId == employeeId && a.LeaveType == type && !a.IsDeleted
                            && (a.Status == LeaveStatus.Approved || a.Status == LeaveStatus.Pending))
                .Select(a => new { a.Status, a.TotalDays })
                .ToListAsync();

            return new LifetimeUsage(
                rows.Count(r => r.Status == LeaveStatus.Approved),
                rows.Count(r => r.Status == LeaveStatus.Pending),
                rows.Where(r => r.Status == LeaveStatus.Approved).Sum(r => r.TotalDays),
                rows.Where(r => r.Status == LeaveStatus.Pending).Sum(r => r.TotalDays));
        }

        public async Task<List<EmployeeYearUsage>> GetUsageForYearAsync(int year)
        {
            var rows = await _context.Set<LeaveApplicationYear>()
                .Where(y => y.Year == year
                            && !y.Application!.IsDeleted
                            && (y.Application.Status == LeaveStatus.Approved || y.Application.Status == LeaveStatus.Pending))
                .GroupBy(y => new { y.Application!.EmployeeId, y.Application.LeaveType })
                .Select(g => new { g.Key.EmployeeId, g.Key.LeaveType, Days = g.Sum(y => y.Days), Cf = g.Sum(y => y.CarryForwardDays) })
                .ToListAsync();
            return rows.Select(r => new EmployeeYearUsage(r.EmployeeId, r.LeaveType, r.Days, r.Cf)).ToList();
        }

        public Task<List<LeaveApplication>> GetApplicationsForYearAsync(int employeeId, int year) =>
            _dbSet
                .AsNoTracking()
                .Include(a => a.Years)
                .Include(a => a.DecidedBy)
                .Where(a => a.EmployeeId == employeeId && !a.IsDeleted && a.Years.Any(y => y.Year == year))
                .OrderByDescending(a => a.FromDate)
                .ToListAsync();

        public Task<List<LeaveApplication>> GetPendingForManagerAsync(int managerEmployeeId) =>
            _dbSet
                .AsNoTracking()
                .Include(a => a.Employee)
                .Where(a => !a.IsDeleted
                            && a.Status == LeaveStatus.Pending
                            && a.Employee!.EmpManagerId == managerEmployeeId)
                .OrderBy(a => a.FromDate)
                .ToListAsync();

        // tracked: DecideAsync changes these and saves through the unit of work
        public Task<List<LeaveApplication>> GetApplicationsByIdsAsync(IEnumerable<int> ids)
        {
            var list = ids.ToList();
            return _dbSet
                .Include(a => a.Employee)
                .Where(a => list.Contains(a.Id) && !a.IsDeleted)
                .ToListAsync();
        }
    }
}
