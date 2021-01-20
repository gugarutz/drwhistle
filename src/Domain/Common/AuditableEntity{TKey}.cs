using System;

namespace DrWhistle.Domain.Common
{
    public abstract class AuditableEntity<TKey> : BaseEntity<TKey>, IAuditableEntity
    {
        public int? CreatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public int? LastModifiedBy { get; set; }

        public DateTimeOffset? LastModifiedOn { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "<>")]
    public abstract class AuditableEntity : AuditableEntity<int>
    {
    }
}