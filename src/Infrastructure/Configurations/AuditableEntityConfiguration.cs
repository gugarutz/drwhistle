using DrWhistle.Domain.Common;
using DrWhistle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrWhistle.Infrastructure.Persistence.Configurations
{
    public abstract class AuditableEntityConfiguration<T> : BaseEntityConfiguration<T>, IEntityTypeConfiguration<T>
        where T : AuditableEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(typeof(ApplicationUser))
                .WithMany()
                .HasForeignKey(nameof(AuditableEntity.CreatedBy))
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(typeof(ApplicationUser))
                .WithMany()
                .HasForeignKey(nameof(AuditableEntity.LastModifiedBy))
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}