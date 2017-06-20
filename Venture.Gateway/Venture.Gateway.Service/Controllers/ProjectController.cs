using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using Venture.Common.Extensions;
using Venture.Gateway.Business.Commands;
using Venture.Gateway.Business.Models;

namespace Venture.Gateway.Service.Controllers
{
    [Route("/v1/projects")]
    public class ProjectController : Controller
    {
        private readonly IBusClient _bus;


        public ProjectController(IBusClient bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProjectCreateModel model)
        {
            var command = new CreateProjectCommand(model.Title, model.Description, model.OwnerId);
            _bus.PublishCommand(command);
            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Patch(Guid id, [FromBody]ProjectUpdateModel model)
        {
            var command = new UpdateProjectCommand(id, model.Title, model.Description);
            _bus.PublishCommand(command);
            return Ok();
        }

        [HttpPatch]
        [Route("{id}/tags")]
        public IActionResult Patch(Guid id, [FromBody]ProjectUpdateTagsModel model)
        {
            var command = new UpdateProjectTagsCommand(id, model.AddTags ?? new List<string>(), model.RemoveTags ?? new List<string>());
            _bus.PublishCommand(command);
            return Ok();
        }

        [HttpPost]
        [Route("{id}/comments")]
        public IActionResult PostComment(Guid id, [FromBody]PostCommentOnProjectModel model)
        {
            var command = new PostCommentOnProjectCommand(id, model.AuthorId, model.Content, DateTime.Now);
            _bus.PublishCommand(command);
            return Ok();
        }
    }
}
