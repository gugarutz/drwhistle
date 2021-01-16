using System;

namespace DrWhistle.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
    }
}
