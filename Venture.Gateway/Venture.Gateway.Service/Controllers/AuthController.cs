using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using Venture.Common.Extensions;
using Venture.Gateway.Business.Commands;
using Venture.Gateway.Business.Models;
using Venture.Gateway.Business.Queries;

namespace Venture.Gateway.Service.Controllers
{
    [Produces("application/json")]
    [Route("/v1/auth")]
    public class AuthController : Controller
    {
        private readonly IBusClient _bus;

        public AuthController(IBusClient bus)
        {
            _bus = bus;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]RegisterModel model)
        {
            var query = new GetUserByNameQuery(model.Name);
            var userWithSameName = _bus.PublishQuery(query);

            if (userWithSameName != "null")
            {
                return BadRequest(new {error = "user with same name already exists."});
            }

            var command = new RegisterUserCommand(model.Name, model.Password);
            _bus.PublishCommand(command);

            return Ok();
        }
    }
}