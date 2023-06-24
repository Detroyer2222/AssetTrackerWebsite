namespace AssetTrackerWebsite.Models.Authentication.Signup;

public class UserSignupDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ComparePassword { get; set; }
    
}