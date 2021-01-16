using System.Collections.Generic;
using System.Threading.Tasks;
using DrWhistle.Application.Common.Models;
using DrWhistle.Domain.Entities;

namespace DrWhistle.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(int userId);

        Task<IEnumerable<Role>> GetRolesAsync();

        Task<(Result Result, int RoleId)> CreateRoleAsync(string roleName);
    }
}