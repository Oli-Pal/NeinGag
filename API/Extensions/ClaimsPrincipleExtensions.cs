using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        //  public static string GetDescription(this ClaimsPrincipal photo)
        // {
        //     return photo.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // }
    }
}