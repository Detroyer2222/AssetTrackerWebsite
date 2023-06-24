namespace AssetTrackerWebsite.Models.Authentication;

public class TokenStore
{
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiration { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiriation { get; set; }
}