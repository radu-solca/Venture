using System;

namespace Venture.Users.Data
{
    public class BaseEntity : IEntity
    {
        public Guid Id { get; }
        public bool Deleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime CreatedAt { get;}

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            Deleted = false;
            DeletedAt = null;
            UpdatedAt = null;
            CreatedAt = DateTime.Now;
        }

        public void Update()
        {
            UpdatedAt = DateTime.Now;
        }

        public void Delete()
        {
            Deleted = true;
            DeletedAt = DateTime.Now;
        }
    }
}
