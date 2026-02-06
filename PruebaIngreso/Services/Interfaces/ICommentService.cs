using PruebaIngreso.DTOs;
using PruebaIngreso.Models;

namespace PruebaIngreso.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetByTaskAsync(Guid taskId);
        Task<Comment?> CreateAsync(Guid taskId, CommentCreateDto dto);
        Task<bool> UpdateAsync(Guid id, CommentUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
