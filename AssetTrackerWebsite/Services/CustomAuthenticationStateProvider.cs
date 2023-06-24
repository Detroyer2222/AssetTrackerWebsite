using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace AssetTrackerWebsite.Services;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ITokenService tokenService;

    public CustomAuthenticationStateProvider(ITokenService tokenService)
    {
        this.tokenService = tokenService;
    }

    public void StateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var tokenStore = await tokenService.GetToken();
        var identity = string.IsNullOrEmpty(tokenStore?.AccessToken) || tokenStore?.AccessTokenExpiration < DateTime.Now
            ? new ClaimsIdentity()
            : new ClaimsIdentity(GetClaimsFromJwt(tokenStore.AccessToken), "jwt");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    private static IEnumerable<Claim> GetClaimsFromJwt(string token)
    {
        var jwt  = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var claims = jwt.Claims;
        return claims;
    }
}