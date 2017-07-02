using System;
using Venture.Common.Data.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Venture.Users.Data
{
    public class User : IdentityUser<Guid>, IEntity
    {
    }
}
