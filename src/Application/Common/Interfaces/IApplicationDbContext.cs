using System.Threading;
using System.Threading.Tasks;
using DrWhistle.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrWhistle.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Role> Roles { get; set; }

        DbSet<ApplicationUser> Users { get; set; }

        DbSet<Case> Cases { get; set; }

        DbSet<Message> Messages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}