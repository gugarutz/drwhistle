using DrWhistle.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrWhistle.Domain.Entities
{
    public class Message : AuditableEntity
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public Case List { get; set; }
    }
}
