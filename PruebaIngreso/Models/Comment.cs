using System;
using System.Collections.Generic;

namespace PruebaIngreso.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        public Guid TaskItemId { get; set; }

        public TaskItem? TaskItem { get; set; }

        public Guid? ParentCommentId { get; set; }

        public Comment? ParentComment { get; set; }

        public ICollection<Comment> Children { get; set; } = new List<Comment>();

        public string CommentText { get; set; } = string.Empty;

        public bool IsUpdated { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
