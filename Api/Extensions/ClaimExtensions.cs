using System.Security.Claims;

namespace Api.Extensions
{
    public static class ClaimExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user){
            return user.FindFirstValue(ClaimTypes.Name);
        }
    }
}