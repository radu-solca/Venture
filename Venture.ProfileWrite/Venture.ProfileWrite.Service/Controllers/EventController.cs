﻿using Microsoft.AspNetCore.Mvc;
using Venture.Common.Cqrs.Queries;
using Venture.ProfileWrite.Business.Queries;

namespace Venture.ProfileWrite.Service.Controllers
{
    [Route("api/events")]
    public class EventController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public EventController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var query = new GetEventsQuery();

            var result = _queryDispatcher.Handle(query);

            return Ok(result);
        }
    }
}
