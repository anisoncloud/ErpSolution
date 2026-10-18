using Erp.Modules.HRM.Enums;
using Erp.Modules.HRM.Services;
using Erp.Web.Areas.HRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Web.Areas.HRM.Controllers
{
    /// <summary>Admin: edit one employee's yearly entitlements, including Earned CF and Compensation.</summary>
    [Area("HRM")]
    //[Authorize(Roles = "Admin")]
    public class EntitlementController : Controller
    {
        private readonly ILeaveService _leave;
        private readonly IHrmClock _clock;

        public EntitlementController(ILeaveService leave, IHrmClock clock)
        {
            _leave = leave;
            _clock = clock;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? year) => View(new EntitlementIndexViewModel
        {
            Year = year ?? _clock.Today.Year,
            Employees = await _leave.GetEmployeesAsync()
        });

        [HttpGet]
        public async Task<IActionResult> Edit(int employeeId, int? year)
        {
            var employee = await _leave.GetEmployeeAsync(employeeId);
            if (employee is null) return NotFound();

            var y = year ?? _clock.Today.Year;
            var t = await _leave.GetEntitlementTotalsAsync(employeeId, y);
            return View(new EntitlementEditViewModel
            {
                EmployeeId = employeeId,
                EmployeeName = employee.FullName,
                Year = y,
                Casual = t[LeaveType.Casual],
                Sick = t[LeaveType.Sick],
                Earned = t[LeaveType.Earned],
                EarnedCarryForward = t[LeaveType.EarnedCarryForward],
                Compensation = t[LeaveType.Compensation]
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EntitlementEditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                await _leave.AssignEntitlementsAsync(model.EmployeeId, model.Year, new Dictionary<LeaveType, int>
                {
                    [LeaveType.Casual] = model.Casual,
                    [LeaveType.Sick] = model.Sick,
                    [LeaveType.Earned] = model.Earned,
                    [LeaveType.EarnedCarryForward] = model.EarnedCarryForward,
                    [LeaveType.Compensation] = model.Compensation
                });
            }
            catch (ArgumentException ex) // includes ArgumentOutOfRangeException (e.g. Earned CF over the limit)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            TempData["Success"] = "Entitlements saved.";
            return RedirectToAction(nameof(Index), new { year = model.Year });
        }
    }
}
