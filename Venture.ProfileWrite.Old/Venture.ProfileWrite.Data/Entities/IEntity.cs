using System;

namespace Venture.ProfileWrite.Data.Entities
{
    public interface IEntity
    {
        Guid Id { get; }
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
        DateTime? DeletedAt { get; }
        bool Deleted { get; }
        void Delete();
        void Update();
    }
}
