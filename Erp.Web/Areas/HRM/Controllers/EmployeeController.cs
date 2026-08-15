using Erp.Core.Identity;
using Erp.Core.Interfaces;
using Erp.Infrastructure.Data;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Enums;
using Erp.Modules.HRM.Services;
using Erp.Web.Areas.HRM.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IEmployeeService _employeeService;
        public EmployeeController(AppDbContext db, UserManager<ApplicationUser> userManager, IEmployeeService employeeService, IUnitOfWork uow)
        {
            _db = db;
            _userManager = userManager;
            _employeeService = employeeService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (IsPriviledgeViewer())
            {
                var employees = await _db.Employees
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .OrderBy(e => e.FullName)
                .ToListAsync();
                return View(employees);
            }
            // Non-Admin/HR (Manager, Executive) never see the full list —
            // send them straight to their own profile instead
            var myRecord = await GetCurrentEmployeeAsync();
            if (myRecord==null)
            {
                return NotFound("No Employee profile linked to this account");
            }
            return RedirectToAction(nameof(Details), new { id = myRecord.Id });

        }
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _db.Employees
                .Include(e=> e.Department)
                .Include (e=> e.Designation)
                .FirstOrDefaultAsync(e=>e.Id==id);
            if (employee==null)
            {
                return NotFound();
            }
            // ---- Resource-based check: Admin/HR bypass, everyone else must own the record ----
            if (!IsPriviledgeViewer())
            {
                var userId = _userManager.GetUserId(User);
                var isOwner = employee.UserId.HasValue
                              && userId != null
                              && employee.UserId.Value == Guid.Parse(userId);

                if (!isOwner)
                    return Forbid(); // 403 — logged in, but not authorized for this specific record
            }

            ViewBag.CanSeeSalary = IsPriviledgeViewer() || User.IsInRole("Manager");
            ViewBag.IsOwnProfile = employee.UserId.HasValue
                && _userManager.GetUserId(User) == employee.UserId.Value.ToString();

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

        private async Task<Employee?> GetCurrentEmployeeAsync()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            return await _db.Employees
                .Include(x => x.Department)
                .Include(x => x.Designation)
                .FirstOrDefaultAsync(e => e.UserId == Guid.Parse(userId));
        }

        private bool IsPriviledgeViewer()
        {
            return User.IsInRole("Admin") || User.IsInRole("HR");
        }
    }
}
