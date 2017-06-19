using System;
using Venture.Common.Cqrs.Commands;

namespace Venture.ProjectWrite.Application
{
    public sealed class CreateProjectCommand : ICommand
    {
        public CreateProjectCommand(string title, string description, Guid ownerId)
        {
            Title = title;
            Description = description;
            OwnerId = ownerId;
        }

        public string Title { get; }
        public string Description { get; }
        public Guid OwnerId { get; }
    }
}
