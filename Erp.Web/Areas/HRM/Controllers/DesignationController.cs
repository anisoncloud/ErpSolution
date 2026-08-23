using Erp.Modules.HRM.DTOs;
using Erp.Modules.HRM.Services;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> Index()
        {
            var dto = await _designationService.GetDesignationAsync();
            return View(dto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new DesignationCreateDto() );
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DesignationCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                await _designationService.CreateDesignation(dto);
                TempData["Success"] = $"Designation '{dto.Title}' created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(dto.Title), ex.Message);
                return View(dto);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var company = await _designationService.GetDesignationByIdAsync(id);
            var dto = new DesignationUpdateDto
            {
                Id = id,
                Title = company.Title,
                Description = company.Description,
                DesignationCode = company.DesignationCode,
                IsActive = company.IsActive,
            };
            return View(dto);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DesignationUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await _designationService.UpdateDesignationAsync(id, dto);
            TempData["Success"] = $"Title '{dto.Title}' Updated successfully.";
            return RedirectToAction("Index");

        }
    }
}
