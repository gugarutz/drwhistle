using DrWhistle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrWhistle.Infrastructure.Persistence.Configurations
{
    public class CaseConfiguration : AuditableEntityConfiguration<Case>
    {
        public override void Configure(EntityTypeBuilder<Case> builder)
        {
            base.Configure(builder);

            builder
                .HasMany<Message>(t => t.Messages)
                .WithOne(a => a.List)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}