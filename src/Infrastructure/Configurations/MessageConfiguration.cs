using DrWhistle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrWhistle.Infrastructure.Persistence.Configurations
{
    public class MessageConfiguration : AuditableEntityConfiguration<Message>
    {
        public override void Configure(EntityTypeBuilder<Message> builder)
        {
            base.Configure(builder);

            builder
                .HasOne<Case>(t => t.List)
                .WithMany(a => a.Messages)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}