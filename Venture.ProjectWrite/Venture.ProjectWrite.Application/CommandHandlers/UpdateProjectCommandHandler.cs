﻿using System;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Data;
using Venture.ProjectWrite.Domain;

namespace Venture.ProjectWrite.Application
{
    public sealed class UpdateProjectCommandHandler : ICommandHandler<UpdateProjectCommand>
    {
        private readonly IRepository<Project> _projectRepository;

        public UpdateProjectCommandHandler(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Handle(UpdateProjectCommand command)
        {
            var project = _projectRepository.Get(command.Id);

            if (project == null || project.Deleted)
            {
                return;
            }

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
            Console.WriteLine("Updated:" + sameProject.Id);
            Console.WriteLine("new title: " + sameProject.Title);
            Console.WriteLine("new descr: " + sameProject.Description);
        }
    }
}
