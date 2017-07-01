using LiteGuard;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Data;
using Venture.TeamWrite.Application.Commands;
using Venture.TeamWrite.Domain;

namespace Venture.TeamWrite.Application.CommandHandlers
{
    public class LeaveTeamCommandHandler : ICommandHandler<LeaveTeamCommand>
    {
        private readonly IRepository<Team> _teamRepository;

        public LeaveTeamCommandHandler(IRepository<Team> teamRepository)
        {
            Guard.AgainstNullArgument(nameof(teamRepository), teamRepository);
            _teamRepository = teamRepository;
        }

        public void Handle(LeaveTeamCommand command)
        {
            Guard.AgainstNullArgument(nameof(command), command);

            var user = new User(command.UserId);
            var team = _teamRepository.Get(command.ProjectId);

            team.Leave(user);

            _teamRepository.Update(team);
        }
    }
}