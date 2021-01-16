using System.Collections.Generic;

namespace DrWhistle.Domain
{
    public static class Constants
    {
        public const string AdministratorRole = "Administrator";

        public static readonly IList<string> SystemRoles = new List<string>()
        {
            AdministratorRole
        }.AsReadOnly();

        public static readonly string DefaultTenantIdentifier = string.Empty;

        public static readonly string RouteTenantParameterName = "__tenant__";
    }
}