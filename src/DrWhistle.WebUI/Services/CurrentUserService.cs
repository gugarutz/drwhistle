using System.Security.Claims;
using DrWhistle.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DrWhistle.Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IOptions<IdentityOptions> identityOptions)
        {
            var value = httpContextAccessor.HttpContext?.User?.FindFirstValue(identityOptions.Value.ClaimsIdentity.UserIdClaimType);

            UserId = !int.TryParse(value, out var id) ? null : (int?)id;
        }

        public int? UserId { get; }
    }
}