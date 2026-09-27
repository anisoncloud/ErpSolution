using Erp.Core.Pagination;
using Erp.Modules.CRM.DTOs;
using Erp.Modules.CRM.MappingDto;
using Erp.Modules.CRM.Services;
using Erp.Modules.CRM.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Web.Areas.CRM.Controllers
{
    [Area("CRM")]
    public class CompanyController : Controller
    {
        private readonly ICrmCompanyService _crmCompanyService;
        private readonly IMotherCompanyService _motherCompanyService;
        public CompanyController(ICrmCompanyService crmCompanyService, IMotherCompanyService motherCompanyService)
        {
            _crmCompanyService = crmCompanyService;
            _motherCompanyService = motherCompanyService;
        }
        /*public async Task<IActionResult> Index()
        {
            var dto = await _crmCompanyService.GetAllCompanys();
            return View(dto);
        }*/
        // GET /Employee?searchTerm=&sortColumn=FullName&sortDirection=asc&pageNumber=1&pageSize=10
        // [FromQuery] binds GridQueryParameters straight off the querystring —
        // this same signature is what every other list controller will look like.
        public async Task<IActionResult> Index([FromQuery] GridQueryParameters parameters)
        {
            parameters.SortColumn ??= "Name";

            var result = await _crmCompanyService.GetAllCompanyListAsync(parameters);
            return View(result);
        }

        // Optional: same data as JSON, e.g. for an AJAX-refreshed table
        // instead of a full page reload. Delete if you don't need it.
        [HttpGet]
        public async Task<IActionResult> IndexJson([FromQuery] GridQueryParameters parameters)
        {
            var result = await _crmCompanyService.GetAllCompanyListAsync(parameters);
            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulaeDropDowns();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            try
            {
                var dto = vm.ToCreateDto();
                await _crmCompanyService.CreateCompany(dto);
                TempData["Success"] = $"Company '{dto.Name}' created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(vm.Name), ex.Message);
                return View(vm);
            }
        }
        private async Task PopulaeDropDowns()
        {
            ViewBag.MotherCompanies = await _motherCompanyService.GetMotherCompanyAsync();
        }
    }
}
