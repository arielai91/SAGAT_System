using Microsoft.EntityFrameworkCore;
using PruebaIngreso.Data;
using PruebaIngreso.DTOs;
using PruebaIngreso.Models;
using PruebaIngreso.Services.Interfaces;

namespace PruebaIngreso.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _db;

        public CommentService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<CommentDto>> GetByTaskAsync(Guid taskId)
        {
            var comments = await _db.Comments
                .Where(c => c.TaskItemId == taskId)
                .AsNoTracking()
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();

            return BuildCommentTree(comments);
        }

        public async Task<Comment?> CreateAsync(Guid taskId, CommentCreateDto dto)
        {
            var taskExists = await _db.TaskItems.AnyAsync(t => t.Id == taskId);
            if (!taskExists) return null;

            if (dto.ParentCommentId is not null)
            {
                var parentExists = await _db.Comments.AnyAsync(c => c.Id == dto.ParentCommentId && c.TaskItemId == taskId);
                if (!parentExists) return null;
            }

            var comment = new Comment
            {
                TaskItemId = taskId,
                ParentCommentId = dto.ParentCommentId,
                CommentText = dto.CommentText
            };

            _db.Comments.Add(comment);
            await _db.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> UpdateAsync(Guid id, CommentUpdateDto dto)
        {
            var comment = await _db.Comments.FindAsync(id);
            if (comment is null) return false;

            comment.CommentText = dto.CommentText;
            comment.IsUpdated = true;
            comment.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var comment = await _db.Comments.FindAsync(id);
            if (comment is null) return false;

            _db.Comments.Remove(comment);
            await _db.SaveChangesAsync();
            return true;
        }

        private static IEnumerable<CommentDto> BuildCommentTree(List<Comment> comments)
        {
            var lookup = comments.ToDictionary(c => c.Id, c => new CommentDto(
                c.Id, c.TaskItemId, c.ParentCommentId, c.CommentText, c.IsUpdated, c.CreatedAt, c.UpdatedAt, new List<CommentDto>()));

            var roots = new List<CommentDto>();

            foreach (var comment in comments)
            {
                if (comment.ParentCommentId is null)
                {
                    roots.Add(lookup[comment.Id]);
                }
                else if (lookup.TryGetValue(comment.ParentCommentId.Value, out var parentDto))
                {
                    (parentDto.Children as List<CommentDto>)?.Add(lookup[comment.Id]);
                }
            }

            return roots;
        }
    }
}
