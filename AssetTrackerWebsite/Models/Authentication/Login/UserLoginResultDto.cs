﻿namespace AssetTrackerWebsite.Models.Authentication.Login;

public class UserLoginResultDto
{
    public int UserId { get; set; }
    public string RefreshToken { get; set; }
    public string AccessToken { get; set; }
}