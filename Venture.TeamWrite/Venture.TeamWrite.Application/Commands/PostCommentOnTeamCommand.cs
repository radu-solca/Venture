using System;
using Venture.Common.Cqrs.Commands;

namespace Venture.TeamWrite.Application.Commands
{
    public sealed class PostCommentOnTeamCommand : ICommand
    {
        public PostCommentOnTeamCommand(Guid projectId, Guid authorId, string content, DateTime postedOn)
        {
            ProjectId = projectId;
            AuthorId = authorId;
            Content = content;
            PostedOn = postedOn;
        }

        public Guid ProjectId { get; }
        public Guid AuthorId { get; }
        public string Content { get; }
        public DateTime PostedOn { get; }
    }
}
