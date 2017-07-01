using System;

namespace Venture.TeamRead.Application.Models
{
    class CommentViewModel
    {
        public Guid Id { get; set; }

        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }

        public string Content { get; set; }
        public DateTime PostedOn { get; set; }
    }
}
