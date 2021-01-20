using DrWhistle.Domain.Common;
using DrWhistle.Domain.Entities;

namespace DrWhistle.Domain.Events
{
    public class MessageCreatedEvent : DomainEvent
    {
        public MessageCreatedEvent(Message item)
        {
            Item = item;
        }

        public Message Item { get; }
    }
}