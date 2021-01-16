using DrWhistle.Domain.Common;

namespace DrWhistle.Domain.Entities
{
    public class Message : AuditableEntity
    {
        public string Content { get; set; }

        public Case List { get; set; }
    }
}