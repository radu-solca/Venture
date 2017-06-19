using System;

namespace Venture.Common.Data
{
    public class Entity
    {
        public Guid Id { get; protected set; }

        public bool IsCreated()
        {
            return Id != Guid.Empty;
        }
    }
}
