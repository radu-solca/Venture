using System;
using Venture.Common.Cqrs.Commands;

namespace Venture.Gateway.Business.Commands
{
    public class LeaveTeamCommand : ICommand
    {
        public LeaveTeamCommand(Guid userId, Guid projectId)
        {
            UserId = userId;
            ProjectId = projectId;
        }

        public Guid UserId { get; }
        public Guid ProjectId { get; }
    }
}