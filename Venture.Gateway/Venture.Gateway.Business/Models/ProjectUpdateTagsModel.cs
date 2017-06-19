using System;
using System.Collections.Generic;

namespace Venture.Gateway.Business.Models
{
    public class ProjectUpdateTagsModel
    {
        public IList<string> AddTags { get; set; }
        public IList<string> RemoveTags { get; set; }
    }
}
