using Microsoft.EntityFrameworkCore;
using PruebaIngreso.Data;
using PruebaIngreso.DTOs;
using PruebaIngreso.Models;
using PruebaIngreso.Services.Interfaces;

namespace PruebaIngreso.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _db;
        private readonly ICommentService _commentService;

        public TaskService(ApplicationDbContext db, ICommentService commentService)
        {
            _db = db;
            _commentService = commentService;
        }

        public async Task<IEnumerable<TaskSummaryDto>> GetAllAsync()
        {
            return await _db.TaskItems
                .Select(t => new TaskSummaryDto(
                    t.Id,
                    t.Title,
                    t.Description,
                    t.IsCompleted,
                    t.CreatedAt,
                    t.UpdatedAt,
                    t.Comments.Count))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TaskWithCommentsDto?> GetByIdAsync(Guid id)
        {
            var task = await _db.TaskItems
                .Include(t => t.Comments)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task is null) return null;

            var comments = await _commentService.GetByTaskAsync(id);

            return new TaskWithCommentsDto(
                task.Id,
                task.Title,
                task.Description,
                task.IsCompleted,
                task.CreatedAt,
                task.UpdatedAt,
                comments);
        }

        public async Task<TaskItem> CreateAsync(TaskCreateDto dto)
        {
            var entity = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description
            };
            _db.TaskItems.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(Guid id, TaskUpdateDto dto)
        {
            var task = await _db.TaskItems.FindAsync(id);
            if (task is null) return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.IsCompleted = dto.IsCompleted;
            task.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var task = await _db.TaskItems.FindAsync(id);
            if (task is null) return false;

            _db.TaskItems.Remove(task);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
