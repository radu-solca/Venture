using System;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Data;
using Venture.ProjectWrite.Domain;

namespace Venture.ProjectWrite.Application
{
    public sealed class UpdateProjectCommandHandler : ICommandHandler<UpdateProjectCommand>
    {
        private readonly Repository<Project> _projectRepository;

        public UpdateProjectCommandHandler(Repository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Execute(UpdateProjectCommand command)
        {
            var project = _projectRepository.Get(command.Id);

            if (!string.IsNullOrEmpty(command.Title))
            {
                project.UpdateTitle(command.Title);
            }

            if (!string.IsNullOrEmpty(command.Description))
            {
                project.UpdateDescription(command.Description);
            }

            _projectRepository.Update(project);


            //DEBUG
            var sameProject = _projectRepository.Get(project.Id);
            Console.WriteLine("updated:" +sameProject.Id);
            Console.WriteLine("new title: " + sameProject.Title);
            Console.WriteLine("new descr: " + sameProject.Description);
        }
    }
}
