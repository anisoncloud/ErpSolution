using Erp.Infrastructure.Data;
using Erp.Infrastructure.Repositories.Generic;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Repositories.HRM
{
    public class DesignationRepository : GenericRepository<Designation>, IDesignationRepository
    {
        public DesignationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Designation?> GetDesignationWithAllEmployee(int id)
        {
            return await _dbSet.Include(x => x.Employees).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Designation?> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            var clearName = name.Replace(" ", "").ToLower().TrimEnd('.');
            return await _dbSet
                .FirstOrDefaultAsync(b =>
                        (b.Title.Replace(" ", "").ToLower().EndsWith(".")
                        ? b.Title.Replace(" ", "").ToLower().Substring(0, b.Title.Replace(" ", "").Length - 1)
                        : b.Title.Replace(" ", "").ToLower())
                            == clearName);
        }
        public async Task<Designation?> GetByCodeAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return null;
            }
            var clearCode = code.Replace(" ", "").ToLower().TrimEnd('.');
            return await _dbSet
                .FirstOrDefaultAsync(b =>
                        (b.DesignationCode.Replace(" ", "").ToLower().EndsWith(".")
                        ? b.DesignationCode.Replace(" ", "").ToLower().Substring(0, b.DesignationCode.Replace(" ", "").Length - 1)
                        : b.DesignationCode.Replace(" ", "").ToLower())
                            == clearCode);
        }

        public async Task<Designation?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
