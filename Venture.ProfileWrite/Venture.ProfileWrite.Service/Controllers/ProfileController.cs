﻿using System;
using Microsoft.AspNetCore.Mvc;
//using Venture.ProfileWrite.Business.CommandDispatcher;
//using Venture.ProfileWrite.Business.Commands;
//using Venture.ProfileWrite.Business.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Venture.ProfileWrite.Service.Controllers
{
    [Route("api/profiles")]
    public class ProfileController : Controller
    {
        //private readonly ICommandDispatcher _commandDispatcher;

        //public ProfileController(ICommandDispatcher commandDispatcher)
        //{
        //    _commandDispatcher = commandDispatcher;
        //}

        //// POST api/values
        //[HttpPost]
        //public IActionResult Post([FromBody] UserProfileCreateModel userProfile)
        //{
        //    var command = new CreateProfileCommand(userProfile);
        //    _commandDispatcher.Handle(command);

        //    return Ok();
        //}

        //[HttpPatch("{id}")]
        //public IActionResult Patch(Guid id, [FromBody] UserProfileUpdateModel userProfile)
        //{
        //    return Ok();
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(Guid id)
        //{
        //    return Ok();
        //}
    }
}