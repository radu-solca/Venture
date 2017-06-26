using System;
using Venture.Common.Data.Interfaces;

namespace Venture.ProjectRead.Data.Entities
{
    public class Project : IEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; }
    }
}
