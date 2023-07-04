using System.Security.Claims;

namespace Utilities;

public static class ClaimsPrincipalExtensions
{
    public static int GetLoggedInUserId(this ClaimsPrincipal principal)
    {
        if (principal == null) throw new ArgumentNullException(nameof(principal));

        var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (loggedInUserId != null) return int.Parse(loggedInUserId);

        throw new Exception("Invalid type provided");
    }

    public static string GetLoggedInUserName(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        return principal.FindFirstValue(ClaimTypes.Name);
    }

    public static string GetLoggedInUserEmail(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        return principal.FindFirstValue(ClaimTypes.Email);
    }
}