using System;
using DrWhistle.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace DrWhistle.Domain.Entities
{
    public class Role : IdentityRole<int>, IEntity<int>
    {
        public Role()
            : base()
        {
        }

        public Role(string roleName)
            : base(roleName)
        {
        }
    }
}