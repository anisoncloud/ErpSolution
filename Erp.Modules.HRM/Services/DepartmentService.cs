using Erp.Modules.HRM.DTOs;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.MappingDto;
using Erp.Modules.HRM.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        public async Task<IEnumerable<DepartmentDto>> GetDepartmentAscSortNameAsync()
        {
            var model = await _uow.Departments.GetAllAsync(c => c.OrderBy(d => d.Name));
            return model.ToListDto();
        }
        public async Task<IEnumerable<DepartmentDto>> GetDepartmentAsync()
        {
            var model = await _uow.Departments.GetAllAsync();
            return model.ToListDto();

        }
        public async Task<DepartmentDto?> GetDepartmentByIdAsync(int id)
        {
            var model = await _uow.Departments.GetDepartmentByIdAsync(id);
            if (model==null)
            {
                return null;
            }
            return model.ToDto();
        }

        public async Task<DepartmentDto> UpdateDepartmentAsync(int id, DepartmentUpdateDto dto)
        {
            var department = await _uow.Departments.GetDepartmentByIdAsync(id);
            if (department is null)
            {
                throw new KeyNotFoundException(
                    $"Department with ID {id} was not found.");
            }
            department.Name = dto.Name.Trim();
            department.Description = dto.Description;
            department.DepartmentCode = dto.DepartmentCode;
            department.IsActive = dto.IsActive;

            await _uow.Departments.UpdateAsync(department);
            await _uow.SaveChangesAsync();
            return department.ToDto();
        }

        ////
        ///
        public async Task<(bool Success, string? Error, int Moved, int Kept)>ChangeDepartmentHeadAsync(int departmentId, int newHeadId)
        {
            var dept = await _uow.Departments.GetByIdTrackedAsync(departmentId);
            if (dept == null)
                return (false, "Department not found.", 0, 0);

            var newHead = await _uow.Employees.GetActiveByIdAsync(newHeadId);
            if (newHead == null)
                return (false, "Selected employee does not exist or is inactive.", 0, 0);

            if (newHead.DepartmentId != departmentId)
                return (false, "The new head must belong to this department.", 0, 0);

            var oldHeadId = dept.HeadEmployeeId;
            if (oldHeadId == newHeadId)
                return (false, "This employee is already the head.", 0, 0);

            int moved = 0, kept = 0;

            if (oldHeadId.HasValue)
            {
                var reports = await _uow.Employees.GetDirectReportsAsync(oldHeadId.Value);

                foreach (var e in reports.Where(r => r.Id != newHeadId))
                {
                    if (e.DepartmentId == departmentId)
                    {
                        e.EmpManagerId = newHeadId;
                        moved++;
                    }
                    else
                    {
                        kept++;
                    }
                }
            }

            newHead.EmpManagerId = null;
            dept.HeadEmployeeId = newHeadId;

            await _uow.SaveChangesAsync();   // one save = atomic
            return (true, null, moved, kept);
        }

        // DepartmentService
        public async Task<DepartmentDto?> GetDepartmentDtoByIdAsync(int id)
        {
            var d = await _uow.Departments.GetDepartmentByIdAsync(id);   // your existing method
            if (d == null) return null;

            return new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                HeadEmployeeId = d.HeadEmployeeId,
                HeadName = d.Head?.FullName            // needs .Include(x => x.Head) in the repository
            };
        }
    }
}
