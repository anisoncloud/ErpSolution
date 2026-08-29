using Erp.Modules.HRM.DTOs;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.MappingDto
{
    public static class EmployeeDtoMapping
    {
        public static EmployeeDto ToDto(this Employee model)
        {
            return new()
            {
                Id = model.Id,
                FullName = model.FullName,
                EmployeeCode = model.EmployeeCode,
                Email = model.Email,
                Phone = model.Phone
            };
        }


        public static List<EmployeeDto> ToListDto(this IEnumerable<Employee> model)
        {
            return model.Select(x => x.ToDto()).ToList();
        }

        public static EmployeeUpdateDto ToEditDto(this Employee model)
        {
            return new()
            {
                Id = model.PublicId,
                 DepartmentId= model.DepartmentId,
                CompanyId = model.CompanyId,
                DesignationId=model.DesignationId,
                IsActive = model.IsActive
            };
        }
        public static EmployeeCreateDto ToCreateDto(this EmployeeFormViewModel empvm)
        {
            return new()
            {
                UserId = empvm.UserId,
                EmployeeCode = empvm.EmployeeCode,
                FullName = empvm.FullName,
                Email = empvm.Email,
                Phone = empvm.Phone,
                DepartmentId = empvm.DepartmentId,
                DesignationId = empvm.DesignationId,
                CompanyId = empvm.CompanyId,
                Level = empvm.Level,
                JoiningDate = empvm.JoiningDate,
                Salary = empvm.Salary,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
