using System;

namespace Venture.Gateway.Business.Models
{
    public sealed class CommentPostModel
    {
        public Guid AuthorId { get; set; }
        public string Content { get; set; }
    }
}
