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
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Company?> GetCompanyWithAllEmployee(int id)
        {
            return await _dbSet.Include(x => x.Employees).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Company?> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            var clearName = name.Replace(" ", "").ToLower().TrimEnd('.');
            return  await _dbSet
                .FirstOrDefaultAsync(b =>
                        (b.Name.Replace(" ", "").ToLower().EndsWith(".")
                        ? b.Name.Replace(" ", "").ToLower().Substring(0, b.Name.Replace(" ", "").Length - 1)
                        : b.Name.Replace(" ", "").ToLower())
                            == clearName);
        }
        public async Task<Company?> GetByCodeAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return null;
            }
            var clearCode = code.Replace(" ", "").ToLower().TrimEnd('.');
            return await _dbSet
                .FirstOrDefaultAsync(b =>
                        (b.CompanyCode.Replace(" ", "").ToLower().EndsWith(".")
                        ? b.CompanyCode.Replace(" ", "").ToLower().Substring(0, b.CompanyCode.Replace(" ", "").Length - 1)
                        : b.CompanyCode.Replace(" ", "").ToLower())
                            == clearCode);
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x=> x.Id == id);
        }
    }
}
