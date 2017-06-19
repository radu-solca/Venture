﻿using System.Collections.Generic;
using Venture.Common.Events;

namespace Venture.Common.Data
{
    public abstract class AggregateRoot : Entity
    {
        public int Version { get; protected set; }
        public IList<DomainEvent> UncommitedChanges { get; }

        protected AggregateRoot()
        {
            UncommitedChanges = new List<DomainEvent>();
        }

        private void ApplyOld(DomainEvent domainEvent)
        {
            
        }

        protected abstract void Apply(DomainEvent domainEvent);

        public void MarkChangesAsCommited()
        {
            UncommitedChanges.Clear();
        }



        //public void LoadFromHistory(IEnumerable<DomainEvent> history)
        //{
        //    foreach (var domainEvent in history)
        //    {
        //        Apply(domainEvent, false);
        //    }
        //}

        //protected void Apply(DomainEvent domainEvent, bool isNew = true)
        //{
        //    if (isNew)
        //    {
        //        UncommitedChanges.Add(domainEvent);
        //    }

        //    ChangeState(domainEvent);
        //}

        //protected abstract void ChangeState(DomainEvent domainEvent);
    }
}
