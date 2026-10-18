using Erp.Modules.HRM.Enums;
using Erp.Modules.HRM.Services;
using Erp.Web.Areas.HRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Web.Areas.HRM.Controllers
{
    /// <summary>Admin: manage the rules of every leave type (yearly days, limits, fixed days, Earned CF limit).</summary>
    [Area("HRM")]
    //[Authorize(Roles = "Admin")]
    public class LeavePolicyController : Controller
    {
        
        private readonly ILeavePolicyService _policies;
        public LeavePolicyController(ILeavePolicyService policies) => _policies = policies;

        [HttpGet]
        public async Task<IActionResult> Index() => View(await _policies.GetAllAsync());

        [HttpGet]
        public async Task<IActionResult> Edit(LeaveType id)
        {
            var p = await _policies.GetAsync(id);
            return p is null ? NotFound() : View(LeavePolicyViewModel.From(p));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LeavePolicyViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                await _policies.UpdateAsync(model.ToDto());
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            TempData["Success"] = $"{model.LeaveType} policy saved. Changes apply to new applications and new years; existing entitlements are edited per employee.";
            return RedirectToAction(nameof(Index));
        }
    }
}
