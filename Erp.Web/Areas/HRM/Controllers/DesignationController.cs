using Erp.Infrastructure.Data;
using Erp.Modules.HRM.DTOs;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Erp.Web.Areas.HRM.Controllers
{
    [Area("HRM")]
    public class DesignationController : Controller
    {
        private readonly IDesignationService _designationService;
        
        public DesignationController(IDesignationService designationService)
        {
            _designationService = designationService;

        }

        /*public async Task<IActionResult> Index()
        {
            var designatins = await _db.Designations.OrderBy(x => x.Title).ToListAsync();
            return View(designatins);
        }*/

        [HttpGet]
        public IActionResult Create()
        {
            return View(new DesignationCreateDto() );
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DesignationCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _designationService.CreateDesignation(dto);                
                return RedirectToAction("Index");
            }            
            return View(dto);
        }
    }
}
