using System.Collections.Generic;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;

namespace DrWhistle.Application.Common.Interfaces
{
    public interface ITenantService<TTenantInfo>
        where TTenantInfo : class, ITenantInfo, new()
    {
        Task<IEnumerable<TTenantInfo>> GetAllAsync();

        Task<TTenantInfo> TryGetAsync(string id);

        Task<TTenantInfo> TryGetByIdentifierAsync(string identifier);
    }
}