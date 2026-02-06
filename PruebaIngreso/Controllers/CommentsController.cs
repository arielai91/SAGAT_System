using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaIngreso.DTOs;
using PruebaIngreso.Services.Interfaces;

namespace PruebaIngreso.Controllers
{
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("api/tasks/{taskId:guid}/comments")]
        public async Task<ActionResult> Create(Guid taskId, CommentCreateDto dto)
        {
            var created = await _commentService.CreateAsync(taskId, dto);
            if (created is null) return NotFound("Task or parent comment not found");
            return Created($"/api/comments/{created.Id}", created);
        }

        [HttpGet("api/tasks/{taskId:guid}/comments")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetByTask(Guid taskId)
        {
            var comments = await _commentService.GetByTaskAsync(taskId);
            return Ok(comments);
        }

        [HttpPut("api/comments/{id:guid}")]
        public async Task<ActionResult> Update(Guid id, CommentUpdateDto dto)
        {
            var updated = await _commentService.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("api/comments/{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleted = await _commentService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
