using DrWhistle.Domain.Common;
using DrWhistle.Domain.Entities;

namespace DrWhistle.Domain.Events
{
    public class CaseCreatedEvent : DomainEvent
    {
        public CaseCreatedEvent(Case item)
        {
            Item = item;
        }

        public Case Item { get; }
    }
}
