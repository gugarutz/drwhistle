using System;

namespace DrWhistle.Domain.Common
{
    public interface IAuditableEntity
    {
        public int? CreatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public int? LastModifiedBy { get; set; }

        public DateTimeOffset? LastModifiedOn { get; set; }
    }
}