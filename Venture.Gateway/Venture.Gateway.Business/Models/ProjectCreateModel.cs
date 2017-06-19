using System;

namespace Venture.Gateway.Business.Models
{
    public class ProjectCreateModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid OwnerId { get; set; }
    }
}
