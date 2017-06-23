using System;
using Venture.Common.Data.Interfaces;

namespace Venture.ProjectRead.Data.Entities
{
    public class Comment : IEntity
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }

        public string Content { get; set; }
        public DateTime PostedOn { get; set; }       
    }
}
