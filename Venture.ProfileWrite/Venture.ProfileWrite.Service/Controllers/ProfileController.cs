using System;
using Microsoft.AspNetCore.Mvc;
using Venture.ProfileWrite.Business.Models;
using Venture.ProfileWrite.Data.Events;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Venture.ProfileWrite.Service.Controllers
{
    [Route("api/profiles")]
    public class ProfileController : Controller
    {
        private readonly IEventStore _eventStore;

        public ProfileController(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("This is a test");
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]ProfileCreateModel profile)
        {
            _eventStore.Raise("ProfileCreated", new { id = Guid.NewGuid(), profile });
            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(Guid id, [FromBody] ProfileCreateModel profile)
        {
            _eventStore.Raise("ProfileUpdated", new{ id, profile });
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _eventStore.Raise("ProfileDeleted", id);
            return Ok();
        }
    }
}
