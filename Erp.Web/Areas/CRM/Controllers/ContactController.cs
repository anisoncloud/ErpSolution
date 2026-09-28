using Erp.Modules.CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Web.Areas.CRM.Controllers
{
    [Area("CRM")]
    public class ContactController : Controller
    {
        private readonly ICrmContactService _crmContactService;
        private readonly ICrmCompanyService _crmCompanyService;
        public ContactController(ICrmContactService crmContactService, ICrmCompanyService crmCompanyService)
        {
            _crmContactService = crmContactService;
            _crmCompanyService = crmCompanyService;
        }
        public async Task<IActionResult> Index()
        {
            //var dtos = await _crmContactService.GetAllContactsAsync();
            var dtos = await _crmCompanyService.GetCompaniesWithContactsAsync();
            return View(dtos);
        }
    }
}
