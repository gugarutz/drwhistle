// using System;
using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace DrWhistle.Infrastructure.Persistence
{
    public class SharedDesignTimeFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // To prep each database uncomment the corresponding line below.
            var tenantInfo = new TenantInfo{ ConnectionString = "Data Source=DESKTOP-PF57I5F;Initial Catalog=dev;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" };
            // var tenantInfo = new TenantInfo{ ConnectionString = "Data Source=Data/InitechIdentity.db" };
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            return new ApplicationDbContext(tenantInfo, optionsBuilder.Options);
        }
    }
}
