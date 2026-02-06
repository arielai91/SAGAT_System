using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaIngreso.DTOs;
using PruebaIngreso.Services.Interfaces;

namespace PruebaIngreso.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskSummaryDto>>> GetAll()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TaskWithCommentsDto>> Get(Guid id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task is null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult> Create(TaskCreateDto dto)
        {
            var entity = await _taskService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, TaskUpdateDto dto)
        {
            var updated = await _taskService.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleted = await _taskService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
