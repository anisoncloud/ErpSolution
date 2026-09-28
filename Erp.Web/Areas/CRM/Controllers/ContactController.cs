using Erp.Modules.CRM.Services;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Web.Areas.CRM.Controllers
{
    [Area("CRM")]
    public class ContactController : Controller
    {
        private readonly ICrmContactService _crmContactService;
        public ContactController(ICrmContactService crmContactService)
        {
            _crmContactService = crmContactService;
        }
        public async Task<IActionResult> Index()
        {
            var dtos = await _crmContactService.GetAllContactsAsync();
            return View(dtos);
        }
    }
}
