using System;

namespace Venture.Common.Data
{
    public class Entity : IEntity
    {
        public Guid Id { get; }

        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }

        public Entity(Guid id, DateTime createdAt)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = CreatedAt;
        }
    }
}
