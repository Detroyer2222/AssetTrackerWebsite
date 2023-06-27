namespace AssetTrackerWebsite.Models.Authentication;

public class AuthenticationDataStorage
{
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiration { get; set; }
}