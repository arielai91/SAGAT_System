using System;
using System.Collections.Generic;

namespace PruebaIngreso.DTOs
{
    public record CommentCreateDto(string CommentText, Guid? ParentCommentId);

    public record CommentUpdateDto(string CommentText);

    public record CommentDto(Guid Id, Guid TaskItemId, Guid? ParentCommentId, string CommentText, bool IsUpdated, DateTime CreatedAt, DateTime? UpdatedAt, IEnumerable<CommentDto> Children);
}
