using System;

namespace Venture.ProjectRead.Data
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid AuthorId { get; set; }
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }
    }
}
