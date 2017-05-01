using System;

namespace Venture.Users.Data
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; internal set; }
        public bool Deleted { get; protected set; }
        public DateTime CreatedAt { get; }
        public DateTime? UpdatedAt { get; protected set; }
        public DateTime? DeletedAt { get; protected set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            Deleted = false;
            CreatedAt = DateTime.Now;
            UpdatedAt = null;
            DeletedAt = null;
        }

        public void Delete()
        {
            Deleted = true;
            DeletedAt = DateTime.Now;
        }

        public void Update()
        {
            UpdatedAt = DateTime.Now;
        }
    }
}
