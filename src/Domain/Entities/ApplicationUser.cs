using DrWhistle.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace DrWhistle.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>, IEntity<int>
    {
        public ApplicationUser()
            : base()
        {
        }

        public ApplicationUser(string userName)
            : base(userName)
        {
        }
    }
}