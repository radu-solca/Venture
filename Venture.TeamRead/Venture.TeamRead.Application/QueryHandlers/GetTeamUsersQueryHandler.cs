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
    public class GetTeamUsersQueryHandler : IQueryHandler<GetTeamUsersQuery>
    {
        private readonly IRepository<TeamMembership> _teamMembershipRepository;

        public GetTeamUsersQueryHandler(IRepository<TeamMembership> teamMembershipRepository)
        {
            _teamMembershipRepository = teamMembershipRepository;
        }

        public string Handle(GetTeamUsersQuery query)
        {
            var teamMemberships = _teamMembershipRepository.Get().Where(m => m.TeamId == query.ProjectId);

            var result = new List<UserViewModel>();
            foreach (var membership in teamMemberships)
            {
                result.Add(new UserViewModel
                {
                    Id = membership.UserId,
                    Name = membership.UserName,
                    Approved = membership.Approved
                });
            }

            return JsonConvert.SerializeObject(result);
        }
    }
}
