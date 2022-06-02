namespace CreditConsult.Web.Common.Extensions;

using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static string GetId(this ClaimsPrincipal user)
    => user.FindFirst(ClaimTypes.NameIdentifier).Value;

    public static bool IsAdministrator(this ClaimsPrincipal user)
        => user.IsInRole("Administrator");
}
