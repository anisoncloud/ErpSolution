using Erp.Core.Identity;
using Erp.Infrastructure.Data;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Enums;
using Erp.Web.Areas.HRM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Erp.Web.Areas.HRM.Controllers
{
    [Area("HRM")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public EmployeeController(AppDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _db.Employees
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .OrderBy(e => e.FullName)
                .ToListAsync();
            return View(employees);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var employee = await _db.Employees
                .Include(e=> e.Department)
                .Include (e=> e.Designation)
                .FirstOrDefaultAsync(e=>e.Id==id);
            if (employee==null)
            {
                return NotFound();
            }
            ViewBag.CanSeeSalary = User.IsInRole("Admin") || User.IsInRole("HR") || User.IsInRole("Manager");
            return View(employee);
        }

        private async Task PopulateDropdows()
        {
            ViewBag.Departments = await _db.Departments.OrderBy(d=>d.Name).ToListAsync();
            ViewBag.Designations = await _db.Designations.OrderBy(d=>d.Title).ToListAsync();
        }

        // Create Employee
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateDropdows();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdows();
                return View(model);
            }
            // Begin the transaction block
            using var transaction = await _db.Database.BeginTransactionAsync();
            // 1. Create the login (ApplicationUser) for the employee
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                Department = model.DepartmentName,
                CreatedAt = DateTime.UtcNow
            };

            var createResult = await _userManager.CreateAsync(user, model.TemporaryPassword);
            if (!createResult.Succeeded)
            {
                foreach (var err in createResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                    await PopulateDropdows();
                    return View(model);
                }
            }

            // 2. Assign the Identity role based on selected level (drives section access)
            var rollName = model.Level == EmployeeLevel.Manager ? "Manager" : "Executive";
            await _userManager.AddToRoleAsync(user, rollName);

            // 3. Create the HRM employee record, linked via UserId
            var employee = new Employee
            {
                UserId = user.Id,
                EmployeeCode = model.EmployeeCode,
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone,
                DepartmentId = model.DepartmentId,
                DesignationId = model.DesignationId,
                Level = model.Level,
                JoiningDate = model.JoiningDate,
                Salary = model.Salary,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();

            // 6. Commit transaction if both steps succeeded
            await transaction.CommitAsync();

            TempData["Success"] = "Employee created successfully.";
            return RedirectToAction(nameof(Index));
        }

    }
}
