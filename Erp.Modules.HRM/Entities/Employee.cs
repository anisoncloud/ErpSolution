using Erp.Core;
using Erp.Core.Identity;
using Erp.Modules.HRM.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Erp.Modules.HRM.Entities
{
    public class Employee : BaseEntity
    {
        public Guid PublicId { get; set; } = Guid.NewGuid();
        // Loose coupling to Identity: store the ApplicationUser's Id as a plain Guid.
        // HRM module does NOT reference ERP.Core.Identity — no FK constraint, no navigation.
        // This keeps HRM's schema self-contained and swappable.
        public Guid? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime JoiningDate { get; set; } = DateTime.UtcNow;
        public DateTime ConfirmationDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? EmployeePhoto {  get; set; }
        public int? EmpManagerId { get; set; }
        public ApplicationUser? Manager { get; set; }
        public string? ManagerEmail {  get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int DesignationId { get; set; }
        public Designation? Designation { get; set; }
        public int CompanyId {  get; set; }
        public Company? Company { get; set; }
        public EmployeeType EmployeeType { get; set; } = EmployeeType.Probation;
        public DutyType DutyType { get; set; }
        public EmployeeLevel Level { get; set; } = EmployeeLevel.Executive;
        public Gender Gender { get; set; }        
        public ICollection<EmployeeAttendance> EmployeeAttendances { get; set; } = new List<EmployeeAttendance>();
       
        //public ICollection<LeaveRequest> LeaveRequests { get; set; }
        

    }
}
