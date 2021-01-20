using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrWhistle.Application.Common.Interfaces;
using Finbuckle.MultiTenant;

namespace DrWhistle.Infrastructure.Services
{
    public class TenantService<TTenantInfo> : ITenantService<TTenantInfo>
        where TTenantInfo : class, ITenantInfo, new()
    {
        private readonly IMultiTenantStore<TTenantInfo> store;

        public TenantService(IMultiTenantStore<TTenantInfo> store)
        {
            this.store = store;
        }

        public async Task<IEnumerable<TTenantInfo>> GetAllAsync()
        {
            return await store.GetAllAsync();
        }

        public Task<bool> TryAddAsync(TTenantInfo tenantInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<TTenantInfo> TryGetAsync(string id)
        {
            return await store.TryGetAsync(id);
        }

        public async Task<TTenantInfo> TryGetByIdentifierAsync(string identifier)
        {
            return await store.TryGetByIdentifierAsync(identifier);
        }

        public Task<bool> TryRemoveAsync(string identifier)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryUpdateAsync(TTenantInfo tenantInfo)
        {
            throw new NotImplementedException();
        }
    }
}