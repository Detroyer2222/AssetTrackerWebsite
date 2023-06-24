namespace AssetTrackerWebsite.Models.Authentication.Login;

public class UserLoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public int OrganizationId { get; set; }
}