using System;
using System.Collections.Generic;

namespace Venture.ProjectRead.Data
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid OwnerId { get; set; }
        public ICollection<string> Tags { get; set; }
    }
}
