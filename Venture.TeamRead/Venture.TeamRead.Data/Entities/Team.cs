using System;
using Venture.Common.Data.Interfaces;

namespace Venture.TeamRead.Data.Entities
{
    public class Team : IEntity
    {
        public Guid Id { get; set; }

        public Guid ProjectOwnerId { get; set; }
    }
}
