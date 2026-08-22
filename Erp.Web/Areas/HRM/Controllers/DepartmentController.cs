using Erp.Infrastructure.Data;
using Erp.Modules.HRM.DTOs;
using Erp.Modules.HRM.Entities;
using Erp.Modules.HRM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Erp.Web.Areas.HRM.Controllers
{
    [Area("HRM")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;

        }

        public async Task<IActionResult> Index()
        {
            var dto = await _departmentService.GetDepartmentAsync();
            return View(dto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new DepartmentCreateDto());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                await _departmentService.CreateDepartment(dto);
                TempData["Success"] = $"Department '{dto.Name}' created successfully.";
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
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            var dto = new DepartmentUpdateDto
            {
                Id = id,
                Name = department.Name,
                Description = department.Description,
                DepartmentCode = department.DepartmentCode,
                IsActive = department.IsActive,
            };
            return View(dto);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await _departmentService.UpdateDepartmentAsync(id, dto);
            TempData["Success"] = $"Department '{dto.Name}' Updated successfully.";
            return RedirectToAction("Index");

        }
    }
}
