using System;
using Venture.Common.Data;

namespace Venture.ProjectWrite.Domain
{
    public sealed class User : Entity
    {
        public string Name { get; }

        public User(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
