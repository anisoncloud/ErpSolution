using Erp.Modules.HRM.DTOs;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.MappingDto;
using Erp.Modules.HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IHrmUnitOfWork _uow;
        public DepartmentService(IHrmUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<DepartmentDto> CreateDepartment(DepartmentCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("Department name can not be empty", nameof(dto.Name));
            }
            var isExists = await _uow.Departments.GetByNameAsync(dto.Name);
            if (isExists != null)
            {
                throw new InvalidOperationException(
                   $"A Department with the name {dto.Name.ToUpper()} is already exists!");
            }
            var isCodeExists = await _uow.Departments.GetByCodeAsync(dto.DepartmentCode);
            if (isCodeExists != null)
            {
                throw new InvalidOperationException(
                   $"A Department Code with the name {dto.DepartmentCode.ToUpper()} is already exists!");
            }
            var model = new Department
            {
                Name = dto.Name.Trim(),
                DepartmentCode = dto.DepartmentCode,
                Description = dto.Description
            };
            await _uow.Departments.AddAsync(model);
            await _uow.SaveChangesAsync();
            return model.ToDto();
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartmentAsync()
        {
            var model = await _uow.Departments.GetAllAsync();
            return model.ToListDto();

        }
        public async Task<DepartmentDto?> GetDepartmentByIdAsync(int id)
        {
            var model = await _uow.Departments.GetByIdAsync(id);
            return model.ToDto();
        }

        public async Task<DepartmentDto> UpdateDepartmentAsync(int id, DepartmentUpdateDto dto)
        {
            var department = await _uow.Departments.GetByIdAsync(id);
            department.Name = dto.Name.Trim();
            department.Description = dto.Description;
            department.DepartmentCode = dto.DepartmentCode;
            department.IsActive = dto.IsActive;

            await _uow.Departments.UpdateAsync(department);
            await _uow.SaveChangesAsync();
            return department.ToDto();
        }
    }
}
