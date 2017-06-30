using System;
using LiteGuard;
using Venture.Common.Data;

namespace Venture.TeamWrite.Domain
{
    public sealed class Comment : Entity
    {
        public User Author { get; }
        public string Content { get; }
        public DateTime PostedOn { get; }

        public Comment(Guid id, User author, string content, DateTime postedOn)
        {
            Guard.AgainstNullArgument(nameof(author), author);
            Guard.AgainstNullArgument(nameof(content), content);

            Id = id;
            Author = author;
            Content = content;
            PostedOn = postedOn;
        }
    }
}
