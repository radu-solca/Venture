using Venture.Common.Cqrs.Commands;
using Venture.Common.Data;
using Venture.ProjectWrite.Application.Commands;
using Venture.ProjectWrite.Domain;

namespace Venture.ProjectWrite.Application.CommandHandlers
{
    public sealed class DeleteProjectCommandHandler : ICommandHandler<DeleteProjectCommand>
    {
        private readonly IRepository<Project> _projectRepository;

        public DeleteProjectCommandHandler(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Handle(DeleteProjectCommand command)
        {
            var project = _projectRepository.Get(command.ProjectId);

            if (project == null || project.Deleted)
            {
                return;
            }

            _projectRepository.Delete(command.ProjectId);
        }
    }
}
