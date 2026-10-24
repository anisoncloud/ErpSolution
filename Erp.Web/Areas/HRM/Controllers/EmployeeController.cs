using Erp.Core.Identity;
using Erp.Core.Interfaces;
using Erp.Infrastructure.Data;
using Erp.Modules.HRM.DTOs;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Enums;
using Erp.Modules.HRM.MappingDto;
using Erp.Modules.HRM.Services;
using Erp.Modules.HRM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;

namespace Erp.Web.Areas.HRM.Controllers
{
    [Area("HRM")]
    public class EmployeeController : Controller
    {
        
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEmployeeService _employeeService;
        private readonly ICompanyService _companyService;
        private readonly IDepartmentService _departmentSerive;
        private readonly IDesignationService _designationService;
        public EmployeeController(AppDbContext db, 
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IEmployeeService employeeService,
            ICompanyService companyService,
            IDepartmentService departmentService,
            IDesignationService designationService,
            IUnitOfWork uow
            )
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _employeeService = employeeService;
            _companyService = companyService;
            _departmentSerive = departmentService;
            _designationService = designationService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (IsPriviledgeViewer())
            {
                var employees = await _db.Employees
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .Include(e=>e.Company)
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
            ViewBag.Companies = await _companyService.GetCompanyAscSortNameAsync();
            ViewBag.Departments = await _departmentSerive.GetDepartmentAscSortNameAsync();
            ViewBag.Designations = await _designationService.GetDesignationAscSortNameAsync();
        }

        // Create Employee
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new EmployeeFormViewModel
            {
                RolesList = await GetRolesAsync()
            };
            await PopulateDropdows();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeFormViewModel model)
        {
            // blank / placeholder manager => null (avoids FK failure on 0)
            if (model.EmpManagerId is null or <= 0)
            {
                model.EmpManagerId = null;
            }
                
            if (!ModelState.IsValid)
            {
                return await ReturnCreateViewAsync(model);
                //await PopulateDropdows();
                //return View(model);
            }
            // Begin the transaction block
            using var transaction = await _db.Database.BeginTransactionAsync();
            // 1. Create the login (ApplicationUser) for the employee
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                DepartmentId = model.DepartmentId,
                CompanyId = model.CompanyId,
                CreatedAt = DateTime.UtcNow
            };

            var createResult = await _userManager.CreateAsync(user, model.TemporaryPassword);
            if (!createResult.Succeeded)
            {
                await transaction.RollbackAsync();
                foreach (var err in createResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                    //await PopulateDropdows();
                    //return View(model);
                    return await ReturnCreateViewAsync(model);
                }
            }

            // 2. Assign the Identity role based on selected level (drives section access)
            /*var rollName = model.Level == EmployeeLevel.Manager ? "Manager" : "Executive";
            await _userManager.AddToRoleAsync(user, rollName);*/
            if (!string.IsNullOrEmpty(model.SelectedRole))
            {
                await _userManager.AddToRoleAsync(user, model.SelectedRole);
            }
            model.UserId = user.Id;
            var dto = model.ToCreateDto();
            var result = await _employeeService.CreateEmployeeAsync(dto);

            /*if (!result.Succeeded)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, result.Error ?? "Could not create employee.");
                return await ReturnCreateViewAsync(model);
            }*/
            
            // 6. Commit transaction if both steps succeeded
            await transaction.CommitAsync();

            TempData["Success"] = "Employee created successfully.";
            return RedirectToAction(nameof(Index));
        }
        private async Task<IActionResult> ReturnCreateViewAsync(EmployeeFormViewModel model)
        {
            model.RolesList = await GetRolesAsync();   // otherwise the role dropdown is empty
            await PopulateDropdows();
            return View(model);
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

        [HttpGet]
        public async Task<IActionResult> GetEmployeesByDepartment(int departmentId)
        {
            var employees = await _db.Employees
                .Where(e => e.DepartmentId == departmentId && e.IsActive)
                .OrderBy(e => e.FullName)
                .Select(e => new { id = e.Id, name = e.FullName, email = e.Email })
                .ToListAsync();

            return Json(employees);
        }

        [HttpGet]
        public async Task<IActionResult> GetManagerCandidates(int? departmentId, bool all = false)
        {
            int? headId = null;
            if (departmentId.HasValue)
            {
                headId = await _db.Departments
                    .Where(d => d.Id == departmentId.Value)
                    .Select(d => d.HeadEmployeeId)
                    .FirstOrDefaultAsync();
            }

            var query = _db.Employees.AsNoTracking().Where(e => e.IsActive);

            if (!all && departmentId.HasValue)
                query = query.Where(e => e.DepartmentId == departmentId.Value);

            var items = await query
                .OrderBy(e => e.Id == headId ? 0 : 1)      // head first
                .ThenBy(e => e.FullName)
                //.OrderBy(e => e.FullName)
                .Select(e => new
                {
                    id = e.Id,
                    name = e.FullName,
                    email = e.Email,
                    departmentId = e.DepartmentId,
                    department = e.Department.Name
                })
                .ToListAsync();

            //return Json(new { headId, items });
            return Json(new { items });
        }
        private async Task<List<SelectListItem>> GetRolesAsync()
        {
            return await _roleManager.Roles
                .Select(r => new SelectListItem 
                { 
                    Text = r.Name, 
                    Value = r.Name 
                }).ToListAsync();
        }

        ///
        [HttpGet]
        public async Task<IActionResult> ChangeHead(int id)
        {
            var dept = await _departmentSerive.GetDepartmentDtoByIdAsync(id);   // returns DepartmentDto?
            if (dept == null) return NotFound();

            ViewBag.Employees = await GetHeadCandidatesAsync(dept);
            return View(dept);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeHead(int id, int? newHeadId)
        {
            if (newHeadId is null or <= 0)
            {
                TempData["Error"] = "Select the new head first.";
                return RedirectToAction(nameof(ChangeHead), new { id });
            }

            var r = await _departmentSerive.ChangeDepartmentHeadAsync(id, newHeadId.Value);
            if (!r.Success)
            {
                TempData["Error"] = r.Error;
                return RedirectToAction(nameof(ChangeHead), new { id });
            }

            TempData["Success"] =
                $"Head changed. {r.Moved} reports moved, {r.Kept} cross-department reports left unchanged.";
            return RedirectToAction(nameof(Index));
        }

        private async Task<List<SelectListItem>> GetHeadCandidatesAsync(DepartmentDto dept)
        {
            var employees = await _employeeService.GetActiveByDepartmentAsync(dept.Id);
            return employees
                .Where(e => e.Id != dept.HeadEmployeeId)
                .OrderBy(e => e.FullName)
                .Select(e => new SelectListItem(e.FullName, e.Id.ToString()))
                .ToList();
        }

    }
}
