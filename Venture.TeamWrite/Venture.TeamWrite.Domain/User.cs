using System;
using Venture.Common.Data;

namespace Venture.TeamWrite.Domain
{
    public class User : Entity
    {
        public bool Approved { get; private set; }

        public User(Guid id)
        {
            Id = id;
            Approved = false;
        }

        public void Approve()
        {
            Approved = true;
        }
    }
}