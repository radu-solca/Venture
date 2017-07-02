using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Semantics;
using Venture.Common.Data;
using Venture.Users.Auth.Models;
using Venture.Users.Data;

namespace Venture.Users.Auth.Controllers
{
    [Produces("application/json")]
    [Route("v1/auth")]
    public class AuthController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly UserManager<User> _userManager;

        public AuthController(IRepository<User> userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [Produces("application/json")]
        [HttpPost("token")]
        public async Task<IActionResult> ExchangeAsync(OpenIdConnectRequest request)
        {
            if (request.IsPasswordGrantType())
            {
                var user = await _userManager.FindByNameAsync(request.Username);

                if (user == null)
                {
                    return Forbid(OpenIdConnectServerDefaults.AuthenticationScheme);
                }

                if (!await _userManager.CheckPasswordAsync(user, request.Password))
                {
                    return Forbid(OpenIdConnectServerDefaults.AuthenticationScheme);
                }
                
                // Create a new ClaimsIdentity holding the user identity.
                var identity = new ClaimsIdentity(
                    OpenIdConnectServerDefaults.AuthenticationScheme,
                    OpenIdConnectConstants.Claims.Name,
                    OpenIdConnectConstants.Claims.Role);

                // Add a "sub" claim containing the user identifier, and attach
                // the "access_token" destination to allow OpenIddict to store it
                // in the access token, so it can be retrieved from your controllers.
                identity.AddClaim(OpenIdConnectConstants.Claims.Subject,
                    user.Id.ToString(),
                    OpenIdConnectConstants.Destinations.AccessToken);

                identity.AddClaim(OpenIdConnectConstants.Claims.Name, 
                    user.UserName,
                    OpenIdConnectConstants.Destinations.AccessToken);

                // ... add other claims, if necessary.
                var principal = new ClaimsPrincipal(identity);

                // Ask OpenIddict to generate a new token and return an OAuth2 token response.
                return SignIn(principal, OpenIdConnectServerDefaults.AuthenticationScheme);
            }

            throw new InvalidOperationException("The specified grant type is not supported.");
        }

        [Produces("application/json")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new User {UserName = model.Name};
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new StatusCodeResult(500);
            }

            return Ok();
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("~/test")]
        public IActionResult Test()
        {
            return Json(new
            {
                Subject = User.GetClaim(OpenIdConnectConstants.Claims.Subject),
                Name = User.GetClaim(OpenIdConnectConstants.Claims.Name)
            });
        }
    }
}