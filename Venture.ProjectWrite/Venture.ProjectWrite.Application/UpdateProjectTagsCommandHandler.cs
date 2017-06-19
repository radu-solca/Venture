using System;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Data;
using Venture.ProjectWrite.Domain;

namespace Venture.ProjectWrite.Application
{
    public sealed class UpdateProjectTagsCommandHandler : ICommandHandler<UpdateProjectTagsCommand>
    {
        private readonly Repository<Project> _projectRepository;

        public UpdateProjectTagsCommandHandler(Repository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Execute(UpdateProjectTagsCommand command)
        {
            var project = _projectRepository.Get(command.Id);

            project.UpdateTags(command.AddTags, command.RemoveTags);

            _projectRepository.Update(project);

            //DEBUG
            var sameProject = _projectRepository.Get(project.Id);
            Console.WriteLine("Updated Tags For: " + sameProject.Title + ":" + sameProject.Id);
            foreach (var tag in sameProject.Tags)
            {
                Console.WriteLine(tag);
            }
        }
    }
}
