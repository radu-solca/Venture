using System;
using Venture.Common.Data.Interfaces;

namespace Venture.Common.Data
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; protected set; }
        public bool Deleted { get; protected set; }

        public virtual void Delete()
        {
            Deleted = true;
        }

        public bool IsCreated()
        {
            return Id != Guid.Empty;
        }
    }
}
