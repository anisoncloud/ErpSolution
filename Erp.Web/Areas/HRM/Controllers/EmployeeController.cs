using Erp.Core.Identity;
using Erp.Infrastructure.Data;
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

    }
}
