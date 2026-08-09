using Erp.Core.Identity;
using Erp.Core.Interfaces;
using Erp.Modules.HRM.DTOs;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public class EmployeeService : IEmployeeService
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHrmUnitOfWork _uow;

        public EmployeeService(IHrmUnitOfWork uow)
        {            
            _uow = uow;
        }

        public async Task<(bool Success, string? Error, Guid EmployeeId)> CreateAsync(EmployeeCreateDto dto)
        {
            if (await _uow.Employees.EmployeeCodeExistsAsync(dto.EmployeeCode))
            {
                return (false, "Employee code is already exists", Guid.Empty);
            }
            if (await _uow.Departments.GetByIdAsync(dto.DepartmentId)==null)
            {
                return (false, "Selected department does not exist.", Guid.Empty);
            }
            if (await _uow.Designations.GetByIdAsync(dto.DesignationId) == null)
            {
                return (false, "Selected designation does not exist.", Guid.Empty);
            }
            var employee = new Employee
            {
                UserId = dto.UserId,
                EmployeeCode = dto.EmployeeCode,
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                DepartmentId = dto.DepartmentId,
                DesignationId = dto.DesignationId,
                Level = dto.Level,
                JoiningDate = dto.JoiningDate,
                Salary = dto.Salary,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            // Single-repo write — plain SaveChangesAsync is enough here.
            // (Multi-repo/cross-module orchestration happens one level up, in the controller —
            // see Section 6, where this call gets wrapped in ExecuteInTransactionAsync alongside
            // the Identity user creation.)
            await _uow.Employees.AddAsync(employee);
            await _uow.SaveChangesAsync();
            return (true, null, employee.Id);
        }

        public Task<(bool Success, string? Error)> DeactivateAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<EmployeeDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto?> GetByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsOwnerAsync(Guid employeeId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<(bool Success, string? Error)> UpdateAsync(EmployeeUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
