using Venture.Common.Cqrs.Queries;
using Venture.ProfileRead.Business.Queries;

namespace Venture.ProfileRead.Business.QueryHandlers
{
    public class GetProfileQueryHandler : IQueryHandler<GetProfileQuery, string>
    {
        public string Retrieve(GetProfileQuery query)
        {
            return "A Profile!";
        }
    }
}
