using System;
using System.Collections.Generic;

namespace PruebaIngreso.DTOs
{
    public record TaskCreateDto(string Title, string? Description);

    public record TaskUpdateDto(string Title, string? Description, bool IsCompleted);

    public record TaskSummaryDto(Guid Id, string Title, string? Description, bool IsCompleted, DateTime CreatedAt, DateTime? UpdatedAt, int CommentCount);

    public record TaskWithCommentsDto(Guid Id, string Title, string? Description, bool IsCompleted, DateTime CreatedAt, DateTime? UpdatedAt, IEnumerable<CommentDto> Comments);
}
