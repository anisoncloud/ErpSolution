using Erp.Modules.HRM.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public interface IEmployeeService
    {
        Task<IReadOnlyList<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto?> GetByIdAsync(Guid id);
        Task<EmployeeDto?> GetByUserIdAsync(Guid userId);
        Task<(bool Success, string? Error, Guid EmployeeId)> CreateAsync(EmployeeCreateDto dto);
        Task<(bool Success, string? Error)> UpdateAsync(EmployeeUpdateDto dto);
        Task<(bool Success, string? Error)> DeactivateAsync(Guid id);
        Task<bool> IsOwnerAsync(Guid employeeId, Guid userId);
    }
}
