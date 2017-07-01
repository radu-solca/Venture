using System;
using Venture.Common.Cqrs.Commands;

namespace Venture.TeamWrite.Application.Commands
{
    public class JoinTeamCommand : ICommand
    {
        public JoinTeamCommand(Guid userId, Guid projectId)
        {
            UserId = userId;
            ProjectId = projectId;
        }

        public Guid UserId { get; }
        public Guid ProjectId { get; }
    }
}
