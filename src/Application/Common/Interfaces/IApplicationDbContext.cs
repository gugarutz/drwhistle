using DrWhistle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DrWhistle.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Case> Cases { get; set; }

        DbSet<Message> Messages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}