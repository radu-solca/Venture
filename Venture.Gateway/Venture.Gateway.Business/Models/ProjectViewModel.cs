using System;

namespace Venture.Gateway.Business.Models
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; }
    }
}
