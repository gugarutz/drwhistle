using System.Collections.Generic;
using System.Threading.Tasks;
using DrWhistle.Application.Common.Interfaces;
using DrWhistle.Application.Common.Models;
using DrWhistle.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DrWhistle.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IOptions<IdentityOptions> identityOptions;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            RoleManager<Role> roleManager,
            IOptions<IdentityOptions> identityOptions)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.identityOptions = identityOptions;
        }

        public async Task<string> GetUserNameAsync(int userId)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            return user?.UserName;
        }

        public async Task<(Result Result, int RoleId)> CreateRoleAsync(string roleName)
        {
            if (await roleManager.RoleExistsAsync(roleName))
            {
                return (Result.Failure("Role already exists"), 0);
            }

            var role = new Role()
            {
                Name = roleName
            };

            var result = await roleManager.CreateAsync(role);

            return (result.ToApplicationResult(), role.Id);
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await roleManager.Roles.ToListAsync();
        }
    }
}