using System;
using Venture.Common.Data.Interfaces;

namespace Venture.Users.Data
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
