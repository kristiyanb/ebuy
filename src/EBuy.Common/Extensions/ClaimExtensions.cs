namespace EBuy.Common.Extensions
{
    using System.Security.Claims;
    using System.Security.Principal;

    public static class ClaimExtensions
    {
        public static string GetClaim(this IIdentity identity, string claim)
        {
            return ((ClaimsIdentity)identity).FindFirst(claim).Value;
        }
    }
}
