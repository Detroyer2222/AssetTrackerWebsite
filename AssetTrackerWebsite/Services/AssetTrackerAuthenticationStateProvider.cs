using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AssetTrackerWebsite.Models.Authentication;
using Microsoft.AspNetCore.Components.Authorization;

namespace AssetTrackerWebsite.Services;

public class AssetTrackerAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ITokenService _tokenService;
    public User CurrentUser { get; private set; } = new();

    public AssetTrackerAuthenticationStateProvider(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public void StateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var tokenStore = await _tokenService.GetJwtToken();
        var identity = String.IsNullOrEmpty(tokenStore?.AccessToken) || tokenStore?.AccessTokenExpiration > DateTime.Now
            ? new ClaimsIdentity()
            : new ClaimsIdentity(GetClaimsFromJwt(tokenStore.AccessToken), "jwt");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        if (tokenStore is not null)
        {
            CurrentUser = User.FromClaimsPrincipal(claimsPrincipal);
            CurrentUser.JwtExpirationDate = String.IsNullOrEmpty(tokenStore?.AccessToken) ? tokenStore.AccessTokenExpiration : DateTime.MinValue;
        }

        return new AuthenticationState(claimsPrincipal);
    }

    private static IEnumerable<Claim> GetClaimsFromJwt(string token)
    {
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var claims = jwt.Claims;
        return claims;
    }
}