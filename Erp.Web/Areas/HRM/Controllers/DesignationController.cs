using Erp.Infrastructure.Data;
using Erp.Modules.HRM.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Erp.Web.Areas.HRM.Controllers
{
    [Area("HRM")]
    public class DesignationController : Controller
    {
        private readonly AppDbContext _db;
        public DesignationController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Designation() );
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Designation model)
        {
            if (ModelState.IsValid)
            {
                _db.Designations.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(model);
        }
    }
}
