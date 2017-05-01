using System;

namespace Venture.Users.Data
{
    public interface IEntity
    {
        Guid Id { get; }

        bool Deleted { get; }

        DateTime CreatedAt { get; }

        DateTime? UpdatedAt { get; }

        DateTime? DeletedAt { get; }

        void Delete();
        void Update();
    }
}
