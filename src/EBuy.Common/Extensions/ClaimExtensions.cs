using System.Security.Claims;
using System.Security.Principal;

namespace EBuy.Common.Extensions
{
    public static class ClaimExtensions
    {
        public static string GetClaim(this IIdentity identity, string claim)
        {
            return ((ClaimsIdentity)identity).FindFirst(claim).Value;
        }
    }
}
