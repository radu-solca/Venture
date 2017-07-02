using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
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
        private readonly UserManager<User> _userManager;

        public AuthController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [Produces("application/json")]
        [HttpPost("token")]
        public async Task<IActionResult> Token(OpenIdConnectRequest request)
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

                // This is here because for some reason, when I try to acess Subject I get a null.
                identity.AddClaim("id",
                    user.Id.ToString(),
                    OpenIdConnectConstants.Destinations.AccessToken);

                // This is here, because even if this always returns null when parsing the claims, it is still required. Some fuckery going on behind the scenes that nulls it maybe.
                identity.AddClaim(OpenIdConnectConstants.Claims.Subject,
                    user.Id.ToString(),
                    OpenIdConnectConstants.Destinations.AccessToken);

                identity.AddClaim(OpenIdConnectConstants.Claims.Name, 
                    user.UserName,
                    OpenIdConnectConstants.Destinations.AccessToken);

                // ... add other claims, if necessary.
                var principal = new ClaimsPrincipal(identity);

                var ticket = new AuthenticationTicket(
                    principal,
                    new AuthenticationProperties(),
                    OpenIdConnectServerDefaults.AuthenticationScheme);

                ticket.SetResources("http://localhost:40000/", "http://localhost:40001/");

                // Ask OpenIddict to generate a new token and return an OAuth2 token response.
                return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
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
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            return Json(new
            {
                Subject = User.GetClaim("id"),
                Name = User.GetClaim(OpenIdConnectConstants.Claims.Name)
            });
        }
    }
}