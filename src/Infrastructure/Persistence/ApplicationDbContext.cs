using DrWhistle.Application.Common.Interfaces;
using DrWhistle.Domain.Common;
using DrWhistle.Domain.Entities;
using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DrWhistle.Infrastructure.Persistence
{
    public class ApplicationDbContext : MultiTenantIdentityDbContext, IApplicationDbContext
    {
        public DbSet<Case> Cases { get; set; }

        public DbSet<Message> Messages { get; set; }

        public ApplicationDbContext(ITenantInfo tenantInfo) : base(tenantInfo)
        {
        }

        public ApplicationDbContext(ITenantInfo tenantInfo, DbContextOptions<ApplicationDbContext> options)
            : base(tenantInfo, options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(TenantInfo.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
