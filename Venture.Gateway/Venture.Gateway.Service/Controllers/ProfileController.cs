using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using Venture.Common.Extensions;
using Venture.Gateway.Business.Commands;
using Venture.Gateway.Business.Queries;

namespace Venture.Gateway.Service.Controllers
{
    [Route("/v1/profiles")]
    public class ProfileController : Controller
    {
        private readonly IBusClient _bus;

        public ProfileController(IBusClient bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public IActionResult GetAsync()
        {
            var query = new GetProfileQuery();
            var result = _bus.PublishQuery<GetProfileQuery, string>(query);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post()
        {
            var command = new CreateProfileCommand("test@test.com", "Testy", "Testinson");
            _bus.PublishCommand(command);
            return Ok();
        }
    }
}
