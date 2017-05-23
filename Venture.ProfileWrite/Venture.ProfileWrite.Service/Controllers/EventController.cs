using Microsoft.AspNetCore.Mvc;
using Venture.ProfileWrite.Business.Queries;
using Venture.ProfileWrite.Business.QueryDispatcher;

//using Venture.ProfileWrite.Data.Events;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
