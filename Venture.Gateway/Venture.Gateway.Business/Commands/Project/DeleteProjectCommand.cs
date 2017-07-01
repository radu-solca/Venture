using System;
using Venture.Common.Cqrs.Commands;

namespace Venture.Gateway.Business.Commands
{
    public sealed class DeleteProjectCommand : ICommand
    {
        public DeleteProjectCommand(Guid projectId)
        {
            ProjectId = projectId;
        }

        public Guid ProjectId { get; }
    }
}
