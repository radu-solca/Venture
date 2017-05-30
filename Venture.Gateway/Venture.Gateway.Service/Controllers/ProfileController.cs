using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using Venture.Gateway.Business.CommandDispatcher;
using Venture.Gateway.Business.Commands;
using Venture.Gateway.Business.Queries;
using Venture.Gateway.Business.QueryDispatcher;

namespace Venture.Gateway.Service.Controllers
{
    [Route("/v1/profiles")]
    public class ProfileController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IBusClient _bus;

        public ProfileController(ICommandDispatcher commandDispatcher, IBusClient bus)
        {
            _commandDispatcher = commandDispatcher;
            _bus = bus;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var query = new GetProfileQuery();
            var result = await _bus.RequestAsync<GetProfileQuery, string>(query);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post()
        {
            var command = new CreateProfileCommand("test@test.com", "Testy", "Testinson");
            _commandDispatcher.DispatchAsync(command);
            return Ok();
        }
    }
}
