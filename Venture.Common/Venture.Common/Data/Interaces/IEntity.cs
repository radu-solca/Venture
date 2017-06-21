using System;

namespace Venture.Common.Data.Interaces
{
    public interface IEntity
    {
        Guid Id { get; }
        bool Deleted { get; }

        void Delete();
    }
}
