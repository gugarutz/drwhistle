using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DrWhistle.Infrastructure.Persistence
{
    public class SharedDesignTimeFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // var tenantInfo = new Tenant { ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=MultiTenant_Megacorp;Trusted_Connection=True;MultipleActiveResultSets=true;" };
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer<ApplicationDbContext>();

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}