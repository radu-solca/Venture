using System;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Data;
using Venture.ProjectWrite.Domain;

namespace Venture.ProjectWrite.Application
{
    public sealed class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
    {
        private readonly IRepository<Project> _projectRepository;

        public CreateProjectCommandHandler(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Handle(CreateProjectCommand command)
        {
            var newProject = new Project();
            newProject.CreateProject(Guid.NewGuid(), command.Title, command.Description, command.OwnerId);

            _projectRepository.Add(newProject);

            //DEBUG
            var sameProject = _projectRepository.Get(newProject.Id);
            Console.WriteLine("Created: " + sameProject.Title);
            Console.WriteLine(sameProject.Id);
        }
    }
}
