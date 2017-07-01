using System;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Data;
using Venture.TeamWrite.Application.Commands;
using Venture.TeamWrite.Domain;

namespace Venture.TeamWrite.Application.CommandHandlers
{
   public sealed class PostCommentOnTeamCommandHandler : ICommandHandler<PostCommentOnTeamCommand>
    {
        private readonly IRepository<Team> _teamRepository;

        public PostCommentOnTeamCommandHandler(IRepository<Team> teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public void Handle(PostCommentOnTeamCommand command)
        {
            var team = _teamRepository.Get(command.ProjectId);

            team.PostComment(
                new Comment(
                    Guid.NewGuid(), 
                    new User(command.AuthorId),
                    command.Content,
                    command.PostedOn));

            _teamRepository.Update(team);
        }
    }
}
