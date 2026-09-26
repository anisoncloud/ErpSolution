using Erp.Modules.CRM.DTOs;
using Erp.Modules.CRM.Services;
using Erp.Modules.HRM.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Web.Areas.CRM.Controllers
{
    [Area("CRM")]
    public class MotherCompanyController : Controller
    {
        private readonly IMotherCompanyService _motherCompanyService;
        public MotherCompanyController(IMotherCompanyService motherCompanyService)
        {
            _motherCompanyService = motherCompanyService;
        }

        public async Task<IActionResult> Index()
        {
            var dto = await _motherCompanyService.GetMotherCompanyAsync();
            return View(dto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MotherCompanyCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                await _motherCompanyService.CreateMotherCompany(dto);
                TempData["Success"] = $"Mother Company '{dto.Name}' created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(dto.Name), ex.Message);
                return View(dto);
            }
        }
    }
}
