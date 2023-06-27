using System.Globalization;
using System.Security.Claims;

namespace AssetTrackerWebsite.Models.Authentication;

public class User
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Id { get; set; }
    public string OrganizationId { get; set; }
    public string OrganizationName { get; set; }
    public DateTime JwtExpirationDate { get; set; }
    public List<string> Roles { get; set; } = new();
    
    public static User FromClaimsPrincipal(ClaimsPrincipal principal) => new()
    {
        Username = principal.FindFirst(ClaimTypes.Name)?.Value ?? String.Empty,
        Email = principal.FindFirst(ClaimTypes.Email)?.Value ?? String.Empty,
        Id = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? String.Empty,
        OrganizationId = principal.FindFirst("OrganizationId")?.Value ?? String.Empty,
        OrganizationName = principal.FindFirst("OrganizationName")?.Value ?? String.Empty,
        Roles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
    };
}