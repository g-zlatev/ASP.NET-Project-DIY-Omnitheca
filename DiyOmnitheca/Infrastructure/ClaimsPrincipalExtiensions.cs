namespace DiyOmnitheca.Infrastructure
{
    using System.Security.Claims;


    public static class ClaimsPrincipalExtiensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
