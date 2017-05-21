using System;

namespace Venture.ProfileWrite.Data.Entities
{
    public class BaseEntity : IEntity
    {
        public Guid Id { get; internal set; }
        public DateTime CreatedAt { get; internal set; }
        public DateTime? UpdatedAt { get; internal set; }
        public DateTime? DeletedAt { get; internal set; }
        public bool Deleted { get; internal set; }

        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
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
