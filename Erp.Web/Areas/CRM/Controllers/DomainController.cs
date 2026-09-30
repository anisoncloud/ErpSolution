using Erp.Modules.CRM.DTOs;
using Erp.Modules.CRM.Services;
using Erp.Modules.CRM.MappingDto;
using Erp.Modules.CRM.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Web.Areas.CRM.Controllers
{
    [Area("CRM")]
    public class DomainController : Controller
    {
        private readonly ICrmCompanyService _companyService;
        private readonly IDomainService _domainService;
        public DomainController(ICrmCompanyService companyService, IDomainService domainService )
        {
            _companyService = companyService;
            _domainService = domainService;
        }

        public async Task<IActionResult> Index()
        {
            var dto = await _domainService.GetAllAsync();
            return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulaeDropDowns();
            return View(new DomainFormViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DomainFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await PopulaeDropDowns();
                return View(vm);
            }
            try
            {
                var dto = vm.ToCreateDto();
                await _domainService.CreateCompany(dto);
                TempData["Success"] = $"Domain '{dto.Name}' created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(vm.Name), ex.Message);
                await PopulaeDropDowns();
                return View(vm);
            }
        }
        private async Task PopulaeDropDowns()
        {
            ViewBag.Companies = await _companyService.GetAllCompanys();
        }
    }
}
