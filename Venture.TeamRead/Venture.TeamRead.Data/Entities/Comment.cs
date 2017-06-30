using System;
using Venture.Common.Data.Interfaces;

namespace Venture.TeamRead.Data.Entities
{
    public class Comment : IEntity
    {
        public Guid Id { get; set; }

        public Guid TeamId { get; set; }
        public Team Team { get; set; }

        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }

        public string Content { get; set; }
        public DateTime PostedOn { get; set; }       
    }
}
