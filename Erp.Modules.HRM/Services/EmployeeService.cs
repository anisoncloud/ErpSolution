using Erp.Core.Interfaces;
using Erp.Modules.HRM.DTOs;
using Erp.Modules.HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDesignationRepository _designationRepository;
        private readonly IUnitOfWork _uow;

        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IDesignationRepository designationRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _designationRepository = designationRepository;
        }

        public Task<(bool Success, string? Error, Guid EmployeeId)> CreateAsync(EmployeeCreateDto dto)
        {
            throw new NotImplementedException();
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
