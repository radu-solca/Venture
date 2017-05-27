using Microsoft.AspNetCore.Mvc;
using Venture.Gateway.Business.CommandDispatcher;
using Venture.Gateway.Business.Commands;

namespace Venture.Gateway.Service.Controllers
{
    [Route("/v1/profiles")]
    public class ProfileController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public ProfileController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
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
