using DrWhistle.Domain.Common;
using DrWhistle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrWhistle.Domain.Events
{
    public class CaseCompletedEvent : DomainEvent
    {
        public CaseCompletedEvent(Case item)
        {
            Item = item;
        }

        public Case Item { get; }
    }
}
