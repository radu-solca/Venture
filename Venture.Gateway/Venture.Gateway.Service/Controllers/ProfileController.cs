using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Cqrs.Queries;
using Venture.Gateway.Business.Commands;
using Venture.Gateway.Business.Queries;

namespace Venture.Gateway.Service.Controllers
{
    [Route("/v1/profiles")]
    public class ProfileController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public ProfileController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public IActionResult GetAsync()
        {
            var query = new GetProfileQuery();
            var result = _queryDispatcher.Handle(query);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post()
        {
            var command = new CreateProfileCommand("test@test.com", "Testy", "Testinson");
            _commandDispatcher.Handle(command);
            return Ok();
        }
    }
}
