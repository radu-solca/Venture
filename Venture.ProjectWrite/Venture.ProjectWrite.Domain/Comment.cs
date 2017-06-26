using System;
using Venture.Common.Data;

namespace Venture.ProjectWrite.Domain
{
    public sealed class Comment : Entity
    {
        public User Author { get; }
        public string Content { get; }
        public DateTime PostedOn { get; }

        public Comment(Guid id, User author, string content, DateTime postedOn)
        {
            Id = id;
            Author = author;
            Content = content;
            PostedOn = postedOn;
        }
    }
}
