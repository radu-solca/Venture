using System;

namespace Venture.Users.Data
{
    public interface IEntity
    {
        Guid Id { get; }

        bool Deleted { get; }

        DateTime? DeletedAt { get; }

        DateTime? UpdatedAt { get; }

        DateTime CreatedAt { get; }
    }
}
