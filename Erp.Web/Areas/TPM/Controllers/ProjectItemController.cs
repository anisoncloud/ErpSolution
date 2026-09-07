using Erp.Modules.HRM.DTOs;
using Erp.Modules.TPM.DTOs;
using Erp.Modules.TPM.Services;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Web.Areas.TPM.Controllers
{
    [Area("tpm")]
    public class ProjectItemController : Controller
    {
        private readonly IProjectItemService _projectItemService;
        public ProjectItemController(IProjectItemService projectItemService)
        {
            _projectItemService = projectItemService;
        }
        public async Task<IActionResult> Index()
        {
            var dto = await _projectItemService.GetCompanyAsync();
            return View(dto);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProjectItemCreateDto());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectItemCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                await _projectItemService.CreateProjectItem(dto);
                TempData["Success"] = $"Project '{dto.Name}' created successfully.";
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
