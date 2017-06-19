using System;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Data;
using Venture.ProjectWrite.Domain;

namespace Venture.ProjectWrite.Application
{
    public sealed class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
    {
        private readonly Repository<Project> _projectRepository;

        public CreateProjectCommandHandler(Repository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Execute(CreateProjectCommand command)
        {
            var newProject = new Project();
            newProject.CreateProject(Guid.NewGuid(), command.Title, command.Description, command.OwnerId);

            _projectRepository.Update(newProject);

            //DEBUG
            var sameProject = _projectRepository.Get(newProject.Id);
            Console.WriteLine("Created: " + sameProject.Title);
            Console.WriteLine(sameProject.Id);
        }
    }
}
