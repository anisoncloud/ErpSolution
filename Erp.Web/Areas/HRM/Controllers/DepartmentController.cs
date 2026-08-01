using Erp.Infrastructure.Data;
using Erp.Modules.HRM.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Erp.Web.Areas.HRM.Controllers
{
    [Area("HRM")]
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _db;
        public DepartmentController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await _db.Departments.OrderBy(x => x.Name).ToListAsync();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Department());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Department model)
        {
            if (ModelState.IsValid)
            {
                _db.Departments.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
