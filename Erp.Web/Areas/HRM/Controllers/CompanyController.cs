using Erp.Modules.HRM.DTOs;
using Erp.Modules.HRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Web.Areas.HRM.Controllers
{
    [Area("hrm")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;

        }

        public async Task<IActionResult> Index()
        {
            var dto = await _companyService.GetCompanyAsync();
            return View(dto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CompanyCreateDto());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                await _companyService.CreateCompany(dto);
                TempData["Success"] = $"Company '{dto.Name}' created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(dto.Name), ex.Message);
                return View(dto);
            }            
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            var dto = new CompanyUpdateDto
            {
                Id = id,
                Name = company.Name,
                Description = company.Description,
                CompanyCode = company.CompanyCode,
                IsActive = company.IsActive,
            };
            return View(dto);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompanyUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await _companyService.UpdateCompanyAsync(id, dto);
            TempData["Success"] = $"Company '{dto.Name}' Updated successfully.";
            return RedirectToAction("Index");

        }

    }
}
