using Erp.Modules.TPM.DTOs;
using Erp.Modules.TPM.Enums;
using Erp.Modules.TPM.Services;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Web.Areas.TPM.Controllers
{
    [Area("TPM")]
    public class ProjectTasksController : Controller
    {
        private readonly IProjectTaskService _projectTaskService;
        public ProjectTasksController(IProjectTaskService projectTaskService)
        {
            _projectTaskService = projectTaskService;
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var task = await _projectTaskService.GetTaskDetailsAsync(id);
            if (task == null) return NotFound();

            return View(task);
        }

        // POST: Tasks/StartDevelopment/5
        [HttpPost]
        public async Task<IActionResult> StartDevelopment(int id)
        {
            await _projectTaskService.UpdateStatusAsync(id, ProjectTaskStatus.InDevelopment);
            return RedirectToAction(nameof(Details), new { id });
        }

        // POST: Tasks/SubmitForReview/5
        [HttpPost]
        public async Task<IActionResult> SubmitForReview(int id)
        {
            await _projectTaskService.UpdateStatusAsync(id, ProjectTaskStatus.ReadyForReview);
            return RedirectToAction(nameof(Details), new { id });
        }

        // POST: Tasks/AddFeedback
        [HttpPost]
        public async Task<IActionResult> AddFeedback(int taskId, string feedbackNotes)
        {
            var dto = new FeedbackCreateDto(taskId, feedbackNotes, "Client");
            await _projectTaskService.AddClientFeedbackAsync(dto);
            return RedirectToAction(nameof(Details), new { id = taskId });
        }

        // POST: Tasks/ResolveUpdate/5?taskId=12
        [HttpPost]
        public async Task<IActionResult> ResolveUpdate(int id, int taskId)
        {
            await _projectTaskService.ResolveRevisionAsync(id);
            return RedirectToAction(nameof(Details), new { id = taskId });
        }

        // POST: Tasks/CompleteTask/5
        [HttpPost]
        public async Task<IActionResult> CompleteTask(int id)
        {
            await _projectTaskService.UpdateStatusAsync(id, ProjectTaskStatus.Completed);
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
