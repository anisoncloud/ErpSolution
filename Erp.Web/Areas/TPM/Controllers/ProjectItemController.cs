using Erp.Core.Contracts.Crm;
using Erp.Modules.HRM.DTOs;
using Erp.Modules.TPM.DTOs;
using Erp.Modules.TPM.Services;
using Erp.Modules.TPM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.InteropServices;

namespace Erp.Web.Areas.TPM.Controllers
{
    [Area("tpm")]
    public class ProjectItemController : Controller
    {
        private readonly IProjectItemService _projectItemService;
        private readonly ICrmLookupService _crmLookupService;
        public ProjectItemController(IProjectItemService projectItemService, ICrmLookupService crmLookupService)
        {
            _projectItemService = projectItemService;
            _crmLookupService = crmLookupService;
        }
        public async Task<IActionResult> Index()
        {
            //var dto = await _projectItemService.GetProjectsAsync();
            var dto = await _projectItemService.GetAllProjectWithOutMaintenance();
            return View(dto);
        }

        public async Task<IActionResult> MaintenanceProject()
        {
            //var dto = await _projectItemService.GetProjectsAsync();
            var dto = await _projectItemService.GetAllMaintenanceProjects();
            return View(dto);
        }
        public async Task<IActionResult> ProjectDashBoard()
        {
            var projectDashboard = await _projectItemService.GetAllProjectsWithTasksAsync();
            if (projectDashboard == null) return NotFound();

            return View(projectDashboard);
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

        [HttpGet]
        public async Task<IActionResult> TaskLogs(int id)
        {
            var projectDashboard = await _projectItemService.GetProjectTaskLogsAsync(id);
            if (projectDashboard == null) return NotFound();

            return View(projectDashboard);
        }

        // GET: Projects/TransitionToMaintenance/5
        [HttpGet]
        public async Task<IActionResult> TransitionToMaintenance(int id)
        {
            var dto = await _projectItemService.GetProjectForMaintenanceEditAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return View(dto);
        }

        // POST: Projects/TransitionToMaintenance
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransitionToMaintenance(ProjectItemMaintenanceEditDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var success = await _projectItemService.UpdateProjectToMaintenanceAsync(dto);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Projects/TransitionToMaintenance/5
        [HttpGet]
        public async Task<IActionResult> TransitionToLive(int id)
        {
            await PopulaeDropDowns();
            var dto = await _projectItemService.GetProjectForMaintenanceEditAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return View(dto);
        }
        // POST: Projects/TransitionToMaintenance
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransitionToLive(ProjectItemMaintenanceEditDto dto)
        {
            ModelState.Remove(nameof(dto.MaintenanceAmount));
            ModelState.Remove(nameof(dto.MaintenanceStartDate));
            if (!ModelState.IsValid)
            {
                await PopulaeDropDowns();
                return View(dto);
            }

            var success = await _projectItemService.UpdateProjectToLiveAsync(dto);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulaeDropDowns() //int? selectedId = null
        {
            ViewBag.Domains = await _crmLookupService.GetCrmDomainAsync();
            //var domains = await _crmLookupService.GetCrmDomainAsync();
            //ViewBag.Domains = new SelectList(domains, "Id", "Name", selectedId);
            /*ViewBag.Domains = domains
            .Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name,
                Selected = d.Id == selectedId
            })
            .ToList();*/
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var projectItem = await _projectItemService.GetProjectItemByIdAsync(id);
            var domain = await _crmLookupService.GetCrmDomainByIdAsync(projectItem.DomainsId ?? 0);

            var vm = new ProjectItemDetailsViewModel
            {
                Name = projectItem.Name,
                DemoStartDate = projectItem.DemoStartDate,
                WorkOrderDate = projectItem.WorkOrderDate,
                ProjectDays = projectItem.ProjectDays,
                ProjectDeliveryDate = projectItem.ProjectDeliveryDate,
                ProjectValue = projectItem.ProjectValue,
                Advanced = projectItem.Advanced,
                ProjectDetails = projectItem.ProjectDetails,
                Comments = projectItem.Comments,
                Proposal = projectItem.Proposal,
                WorkOrder = projectItem.WorkOrder,
                SoftwareRequirement = projectItem.SoftwareRequirement,
                ProjectItemType = projectItem.ProjectItemType,
                ProjectItemStatus = projectItem.ProjectStatus,
                LiveServerDate = projectItem.LiveServerDate,
                MaintValue = projectItem.MaintValue,
                MaintStartDate = projectItem.MaintStartDate,
                DomainName = domain.Name
            };
            return View(vm);
        }

    }

}
