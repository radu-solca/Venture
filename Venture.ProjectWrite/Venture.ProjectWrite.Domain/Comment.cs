using System;
using Venture.Common.Data;

namespace Venture.ProjectWrite.Domain
{
    public sealed class Comment : Entity
    {
        public Guid AuthorId { get; }
        public string Content { get; }
        public DateTime PostedOn { get; }

        public Comment(Guid id, Guid authorId, string content, DateTime postedOn)
        {
            AuthorId = authorId;
            Content = content;
            PostedOn = postedOn;
            Id = id;
        }
    }
}
