using Erp.Modules.HRM.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetDepartmentAsync();
        Task<DepartmentDto> CreateDepartment(DepartmentCreateDto dto);
        Task<DepartmentDto?> GetDepartmentByIdAsync(int id);
        Task<DepartmentDto> UpdateDepartmentAsync(int id, DepartmentUpdateDto dto);
    }
}
