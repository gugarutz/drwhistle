using DrWhistle.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrWhistle.Domain.Entities
{
    public class Case : AuditableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IList<Message> Messages { get; private set; } = new List<Message>();
    }
}
