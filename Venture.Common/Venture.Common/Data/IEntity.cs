using System;

namespace Venture.Common.Data
{
    public interface IEntity
    {
        Guid Id { get; }

        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }
    }
}
