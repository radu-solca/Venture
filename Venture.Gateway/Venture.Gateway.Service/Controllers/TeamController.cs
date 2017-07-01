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
    [Route("v1/teams")]
    public class TeamController : Controller
    {
        private readonly IBusClient _bus;

        public TeamController(IBusClient bus)
        {
            _bus = bus;
        }

        [HttpPost]
        [Route("{id}/users")]
        public IActionResult Get(Guid id)
        {
            var query = new GetTeamUsersQuery(id);
            var result = _bus.PublishQuery(query);

            if (result == null)
            {
                return NotFound();
            }

            var project = JsonConvert.DeserializeObject<ProjectViewModel>(result);

            return Ok(project);
        }

        [HttpPost]
        [Route("{id}/users")]
        public IActionResult Join(Guid id, [FromBody]Guid userId)
        {
            var command = new JoinTeamCommand(userId, id);
            _bus.PublishCommand(command);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}/users/{userId}")]
        public IActionResult Leave(Guid id, Guid userId)
        {
            var command = new LeaveTeamCommand(userId, id);
            _bus.PublishCommand(command);
            return Ok();
        }

        [HttpPost]
        [Route("{id}/users/{userId}/approval")]
        public IActionResult Approve(Guid id, Guid userId)
        {
            var command = new ApproveTeamUserCommand(userId, id);
            _bus.PublishCommand(command);
            return Ok();
        }

        [HttpGet]
        [Route("{id}/chat")]
        public IActionResult GetComments(Guid id)
        {
            var query = new GetTeamCommentsQuery(id);
            var result = _bus.PublishQuery(query);

            if (result == null)
            {
                return NotFound();
            }

            var chat = JsonConvert.DeserializeObject<List<CommentViewModel>>(result);

            return Ok(chat);
        }

        [HttpPost]
        [Route("{id}/chat")]
        public IActionResult PostComment(Guid id, [FromBody]CommentPostModel model)
        {
            var command = new PostCommentOnTeamCommand(id, model.AuthorId, model.Content, DateTime.Now);
            _bus.PublishCommand(command);
            return Ok();
        }
    }
}