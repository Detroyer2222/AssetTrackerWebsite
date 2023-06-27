namespace AssetTrackerWebsite.Models.Authentication
{
    public class RefreshTokenStorage
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiriation { get; set; }
    }
}
