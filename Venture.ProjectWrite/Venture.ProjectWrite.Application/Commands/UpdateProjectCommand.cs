using System;
using Venture.Common.Cqrs.Commands;

namespace Venture.ProjectWrite.Application
{
    public class UpdateProjectCommand : ICommand
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }

        public UpdateProjectCommand(Guid id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
    }
}
