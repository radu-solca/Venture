﻿using System;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Data;
using Venture.ProjectWrite.Domain;

namespace Venture.ProjectWrite.Application
{
   public sealed class PostCommentOnProjectCommandHandler : ICommandHandler<PostCommentOnProjectCommand>
    {
        private readonly Repository<Project> _projectRepository;

        public PostCommentOnProjectCommandHandler(Repository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Handle(PostCommentOnProjectCommand command)
        {
            var project = _projectRepository.Get(command.ProjectId);
            project.PostComment(command.AuthorId,command.Content,command.PostedOn);

            _projectRepository.Update(project);

            //Debug
            var sameProject = _projectRepository.Get(project.Id);
            Console.WriteLine("Comment posted on: " + sameProject.Id);
            foreach (var comment in sameProject.Chat)
            {
                Console.WriteLine(comment.AuthorId + ": " + comment.Content + " on " + comment.PostedOn);
            }
        }
    }
}
