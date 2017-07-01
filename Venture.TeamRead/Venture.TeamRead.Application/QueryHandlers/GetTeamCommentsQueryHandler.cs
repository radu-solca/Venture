using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Venture.Common.Cqrs.Queries;
using Venture.Common.Data;
using Venture.TeamRead.Application.Models;
using Venture.TeamRead.Application.Queries;
using Venture.TeamRead.Data.Entities;

namespace Venture.TeamRead.Application.QueryHandlers
{
    public class GetTeamCommentsQueryHandler : IQueryHandler<GetTeamCommentsQuery>
    {
        private readonly IRepository<Comment> _commentRepository;

        public GetTeamCommentsQueryHandler(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public string Handle(GetTeamCommentsQuery query)
        {
            var comments = Enumerable.Where(_commentRepository.Get(), m => m.TeamId == query.ProjectId);

            var result = new List<CommentViewModel>();
            foreach (var comment in comments)
            {
                result.Add(new CommentViewModel
                {
                    Id = comment.Id,
                    AuthorId = comment.AuthorId,
                    AuthorName = comment.AuthorName,
                    Content = comment.Content,
                    PostedOn = comment.PostedOn
                });
            }

            return JsonConvert.SerializeObject(result);
        }
    }
}