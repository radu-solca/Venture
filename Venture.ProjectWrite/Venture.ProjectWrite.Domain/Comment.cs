using System;
using System.Reflection.Metadata;
using Venture.Common.Data;

namespace Venture.ProjectWrite.Domain
{
    public sealed class Comment : Entity
    {
        public User Author { get; }
        public string Content { get; }


        public Comment(Guid id, User author, string content)
        {
            Author = author;
            Content = content;
            Id = id;
        }
    }
}
