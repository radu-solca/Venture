using LiteGuard;
using Newtonsoft.Json;
using Venture.Common.Cqrs.Queries;
using Venture.Common.Data;
using Venture.ProjectRead.Application.Queries;
using Venture.ProjectRead.Data.Entities;

namespace Venture.ProjectRead.Application.QueryHandlers
{
    public sealed class GetProjectsQueryHandler : IQueryHandler<GetProjectsQuery>
    {
        private readonly IRepository<Project> _projectRepository;

        public GetProjectsQueryHandler(IRepository<Project> projectRepository)
        {
            Guard.AgainstNullArgument(nameof(projectRepository), projectRepository);

            _projectRepository = projectRepository;
        }

        public string Handle(GetProjectsQuery query)
        {
            Guard.AgainstNullArgument(nameof(query), query);

            var project = _projectRepository.Get();
            return JsonConvert.SerializeObject(project);
        }
    }
}