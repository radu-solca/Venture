using System;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using Venture.Common.Extensions;
using Venture.Gateway.Business.Commands;

namespace Venture.Gateway.Service.Controllers
{
    [Route("/v1/projects")]
    public class ProjectController : Controller
    {
        private readonly IBusClient _bus;

        private static int DELETEMECOUNTER = 0;

        public ProjectController(IBusClient bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public IActionResult Post()
        {
            var command = new CreateProjectCommand("Title", "Description");
            _bus.Command(command);
            return Ok();
        }

        [HttpPatch]
        public IActionResult Patch(Guid id, string title, string description)
        {
            var command = new UpdateProjectCommand(new Guid("9f285030-89ec-48f1-8b3e-28042dc1d972"), "Updated Title " + (DELETEMECOUNTER ++), "Desc");
            _bus.Command(command);
            return Ok();
        }
    }
}
