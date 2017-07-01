using System;
using Venture.Common.Cqrs.Commands;

namespace Venture.Gateway.Business.Commands
{
    public class ApproveTeamUserCommand : ICommand
    {
        public ApproveTeamUserCommand(Guid userId, Guid projectId)
        {
            UserId = userId;
            ProjectId = projectId;
        }

        public Guid UserId { get; }
        public Guid ProjectId { get; }
    }
}