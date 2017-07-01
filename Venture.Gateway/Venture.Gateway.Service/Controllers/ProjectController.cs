using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RawRabbit;
using Venture.Common.Extensions;
using Venture.Gateway.Business.Commands;
using Venture.Gateway.Business.Models;
using Venture.Gateway.Business.Queries;

namespace Venture.Gateway.Service.Controllers
{
    [Produces("application/json")]
    [Route("/v1/projects")]
    public class ProjectController : Controller
    {
        private readonly IBusClient _bus;

        public ProjectController(IBusClient bus)
        {
            _bus = bus;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(Guid id)
        {
            var query = new GetProjectQuery(id);
            var result = _bus.PublishQuery(query);

            if (result == null)
            {
                return NotFound();
            }

            var project = JsonConvert.DeserializeObject<ProjectViewModel>(result);

            return Ok(project);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var query = new GetProjectsQuery();
            var result = _bus.PublishQuery(query);

            if (result == null)
            {
                return NotFound();
            }

            var project = JsonConvert.DeserializeObject<List<ProjectViewModel>>(result);

            return Ok(project);
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

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            var command = new DeleteProjectCommand(id);
            _bus.PublishCommand(command);
            return Ok();
        }

        [HttpPost]
        [Route("{id}/chat")]
        public IActionResult PostComment(Guid id, [FromBody]CommentPostModel model)
        {
            var command = new PostCommentOnProjectCommand(id, model.AuthorId, model.Content, DateTime.Now);
            _bus.PublishCommand(command);
            return Ok();
        }

        [HttpGet]
        [Route("{id}/chat")]
        public IActionResult GetComments(Guid id)
        {
            var query = new GetProjectCommentsQuery(id);
            var result = _bus.PublishQuery(query);

            var chat = JsonConvert.DeserializeObject<List<CommentViewModel>>(result);

            return Ok(chat);
        }
    }
}
