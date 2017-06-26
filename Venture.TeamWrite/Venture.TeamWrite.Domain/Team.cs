using System.Collections.Generic;
using Venture.Common.Data;
using Venture.Common.Events;

namespace Venture.TeamWrite.Domain
{
    public class Team : AggregateRoot
    {
        public User ProjectOwner { get; private set; }
        public ICollection<User> Users { get; private set; }
        public ICollection<Comment> Chat { get; private set; }

        public void Create()
        {
            
        }

        public override void Delete()
        {
            throw new System.NotImplementedException();
        }

        protected override void ChangeState(DomainEvent domainEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}
