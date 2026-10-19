using Erp.Core.Identity;
using Erp.Modules.HRM.Repositories;
using Erp.Modules.HRM.Services;
using Erp.Web.Areas.HRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Web.Areas.HRM.Controllers
{
    [Area("HRM")]
    //[Authorize(Roles = "Admin,Manager,Employee")]
    public class LeaveController : Controller
    {
        private readonly ILeaveService _leave;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHrmClock _clock;

        public LeaveController(ILeaveService leave, UserManager<ApplicationUser> userManager, IHrmClock clock)
        {
            _leave = leave;
            _userManager = userManager;
            _clock = clock;
        }

        private async Task<EmployeeRef?> CurrentEmployeeAsync()
        {
            var id = _userManager.GetUserId(User);
            return Guid.TryParse(id, out var userId) ? await _leave.GetEmployeeByUserAsync(userId) : null;
        }

        /// <summary>Renders the form with the leave types this employee may apply for (gender rules applied).</summary>
        private async Task<IActionResult> ApplyViewAsync(EmployeeRef me, LeaveApplyViewModel model)
        {
            ViewBag.Types = await _leave.GetApplicableTypesAsync(me.Id);
            return View("Apply", model);
        }

        // ---------------------------------------------------- apply (step 1)

        [HttpGet]
        public async Task<IActionResult> Apply()
        {
            var me = await CurrentEmployeeAsync();
            if (me is null) return NotFound("No Employee profile linked to this account");
            return await ApplyViewAsync(me, new LeaveApplyViewModel());
        }

        // ------------------------------------------- confirmation (step 2)

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(LeaveApplyViewModel model)
        {
            var me = await CurrentEmployeeAsync();
            if (me is null) return NotFound("No Employee profile linked to this account");
            if (!ModelState.IsValid) return await ApplyViewAsync(me, model);

            var preview = await _leave.PreviewAsync(me.Id, model.LeaveType!.Value, model.FromDate!.Value, model.ReturnDate);
            if (!preview.IsValid)
            {
                foreach (var e in preview.Errors) ModelState.AddModelError(string.Empty, e);
                return await ApplyViewAsync(me, model);
            }

            // Show the calculated return date (fixed-day leave) rather than whatever was typed.
            model.ReturnDate = preview.ReturnDate;
            return View("Confirm", new LeaveConfirmViewModel { Input = model, Preview = preview });
        }

        /// <summary>Back button on the confirmation page: return to the form with the values kept.</summary>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Back(LeaveApplyViewModel model)
        {
            var me = await CurrentEmployeeAsync();
            if (me is null) return NotFound("No Employee profile linked to this account");
            return await ApplyViewAsync(me, model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(LeaveApplyViewModel model)
        {
            var me = await CurrentEmployeeAsync();
            if (me is null) return NotFound("No Employee profile linked to this account");
            if (!ModelState.IsValid) return await ApplyViewAsync(me, model);

            // Everything is re-validated and re-counted here; the hidden fields (and any days value) are never trusted.
            var result = await _leave.SubmitAsync(me.Id, model.LeaveType!.Value, model.FromDate!.Value, model.ReturnDate);
            if (result.ApplicationId is null)
            {
                foreach (var e in result.Preview.Errors) ModelState.AddModelError(string.Empty, e);
                return await ApplyViewAsync(me, model);
            }

            TempData["Success"] = "Leave application submitted and is pending approval.";
            return RedirectToAction(nameof(Report), new { year = model.FromDate!.Value.Year });
        }

        // --------------------------------------------------------- report

        [HttpGet]
        public async Task<IActionResult> Report(int? year)
        {
            var me = await CurrentEmployeeAsync();
            if (me is null) return NotFound("No Employee profile linked to this account");

            var report = await _leave.GetReportAsync(me.Id, year ?? _clock.Today.Year);
            return View(report);
        }

        // ------------------------------------------------------- approvals

        [HttpGet, Authorize(Roles = "Manager")]
        public async Task<IActionResult> Approvals()
        {
            var me = await CurrentEmployeeAsync();
            if (me is null) return NotFound("No Employee profile linked to this account");

            return View(new LeaveApprovalsViewModel { Items = await _leave.GetPendingForManagerAsync(me.Id) });
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Manager")]
        public async Task<IActionResult> Approvals(int[] selectedIds, string decision, string? comment)
        {
            var me = await CurrentEmployeeAsync();
            if (me is null) return NotFound("No Employee profile linked to this account");

            if (selectedIds is null || selectedIds.Length == 0)
            {
                TempData["Error"] = "Select at least one application.";
                return RedirectToAction(nameof(Approvals));
            }

            var approve = string.Equals(decision, "approve", StringComparison.OrdinalIgnoreCase);
            var result = await _leave.DecideAsync(me.Id, selectedIds, approve, comment);

            if (result.Processed > 0)
                TempData["Success"] = $"{result.Processed} application(s) {(approve ? "approved" : "declined")}.";
            if (result.Errors.Count > 0)
                TempData["Error"] = string.Join(" ", result.Errors);

            return RedirectToAction(nameof(Approvals));
        }
    }
}
