using PruebaIngreso.DTOs;
using PruebaIngreso.Models;

namespace PruebaIngreso.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskSummaryDto>> GetAllAsync();
        Task<TaskWithCommentsDto?> GetByIdAsync(Guid id);
        Task<TaskItem> CreateAsync(TaskCreateDto dto);
        Task<bool> UpdateAsync(Guid id, TaskUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
