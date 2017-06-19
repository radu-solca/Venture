using System;
using System.Collections.Generic;
using Venture.Common.Cqrs.Commands;

namespace Venture.Gateway.Business.Commands
{
    public sealed class UpdateProjectTagsCommand : ICommand
    {
        public Guid Id { get; }
        public IList<string> AddTags { get; }
        public IList<string> RemoveTags { get; }

        public UpdateProjectTagsCommand(Guid id, IList<string> addTags, IList<string> removeTags)
        {
            Id = id;
            AddTags = addTags;
            RemoveTags = removeTags;
        }
    }
}
