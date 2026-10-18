using Erp.Modules.HRM.Services;
using Erp.Web.Areas.HRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Web.Areas.HRM.Controllers
{
    [Area("HRM")]
    //[Authorize(Roles = "Admin")]
    public class HolidayController : Controller
    {
        private readonly IHolidayService _holidays;
        private readonly IHrmClock _clock;

        public HolidayController(IHolidayService holidays, IHrmClock clock)
        {
            _holidays = holidays;
            _clock = clock;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? year)
        {
            var y = year ?? _clock.Today.Year;
            return View(new HolidayIndexViewModel { Year = y, Holidays = await _holidays.GetByYearAsync(y) });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(Prefix = "NewHoliday")] HolidayFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please enter a valid date and name.";
                return RedirectToAction(nameof(Index));
            }
            try
            {
                await _holidays.CreateAsync(model.Date!.Value, model.Name);
                TempData["Success"] = "Holiday added.";
            }
            catch (InvalidOperationException ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction(nameof(Index), new { year = model.Date!.Value.Year });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var h = await _holidays.GetByIdAsync(id);
            if (h is null) return NotFound();
            return View(new HolidayFormViewModel { Id = h.Id, Date = h.Date, Name = h.Name });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(HolidayFormViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                await _holidays.UpdateAsync(model.Id, model.Date!.Value, model.Name);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            TempData["Success"] = "Holiday updated.";
            return RedirectToAction(nameof(Index), new { year = model.Date!.Value.Year });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int year)
        {
            try { await _holidays.DeleteAsync(id); TempData["Success"] = "Holiday deleted."; }
            catch (InvalidOperationException ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction(nameof(Index), new { year });
        }
    }
}
