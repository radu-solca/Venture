using LiteGuard;
using Newtonsoft.Json;
using Venture.Common.Cqrs.Queries;
using Venture.Common.Data;
using Venture.ProjectRead.Application.Queries;
using Venture.ProjectRead.Data;
using Venture.ProjectRead.Data.Entities;

namespace Venture.ProjectRead.Application.QueryHandlers
{
    public class GetProjectCommentsQueryHandler : IQueryHandler<GetProjectCommentsQuery>
    {
        private readonly CommentRepository _commentRepository;
        private readonly IRepository<Project> _projectRepository;

        public GetProjectCommentsQueryHandler(CommentRepository commentRepository, IRepository<Project> projectRepository)
        {
            Guard.AgainstNullArgument(nameof(commentRepository), commentRepository);
            Guard.AgainstNullArgument(nameof(projectRepository), projectRepository);

            _commentRepository = commentRepository;
            _projectRepository = projectRepository;
        }

        /// <returns>A collection of comments belonging to the specified project, JSON encoded. 
        /// If there are no comments, then return an empty list.
        /// If the project does not exist, then return null.</returns>
        public string Handle(GetProjectCommentsQuery query)
        {
            Guard.AgainstNullArgument(nameof(query), query);

            if (_projectRepository.Get(query.ProjectId) == null)
            {
                return "";
            }

            var comments = _commentRepository.GetByProjectId(query.ProjectId);
            return JsonConvert.SerializeObject(comments);
        }
    }
}