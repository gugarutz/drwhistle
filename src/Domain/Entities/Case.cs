using System.Collections.Generic;
using DrWhistle.Domain.Common;

namespace DrWhistle.Domain.Entities
{
    public class Case : AuditableEntity
    {
        public string Title { get; set; }

        public IList<Message> Messages { get; private set; } = new List<Message>();
    }
}