using AssetTrackerWebsite.Models.Authentication;
using Blazored.LocalStorage;
using Blazored.SessionStorage;

namespace AssetTrackerWebsite.Services;

public interface ITokenService
{
    Task<AuthenticationDataStorage> GetJwtToken();
    Task RemoveJwtToken();
    Task SetJwtToken(string token, DateTime expiration);

    Task<RefreshTokenStorage> GetRefreshToken();
    Task RemoveRefreshToken();
    Task SetRefreshToken(string token, DateTime expiration);
}

public class TokenService : ITokenService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly ISessionStorageService _sessionStorageService;

    public TokenService(ILocalStorageService localStorageService, ISessionStorageService sessionStorageService)
    {
        this._localStorageService = localStorageService;
        _sessionStorageService = sessionStorageService;
    }

    public async Task SetJwtToken(string token, DateTime expiration)
    {
        //TODO - Add expiration date to token
        await _sessionStorageService.SetItemAsync("jwtToken", new AuthenticationDataStorage
        {
            AccessToken = token,
            AccessTokenExpiration = expiration,
        });
    }

    public async Task<AuthenticationDataStorage> GetJwtToken()
    {
        return await _sessionStorageService.GetItemAsync<AuthenticationDataStorage>("jwtToken");
    }

    public async Task RemoveJwtToken()
    {
        await _sessionStorageService.RemoveItemAsync("token");
    }

    public async Task SetRefreshToken(string token, DateTime expiration)
    {
        await _localStorageService.SetItemAsync("refreshToken", new RefreshTokenStorage
        {
            RefreshToken = token,
            RefreshTokenExpiriation = expiration,
        });
    }

    public async Task<RefreshTokenStorage> GetRefreshToken()
    {
        return await _localStorageService.GetItemAsync<RefreshTokenStorage>("refreshToken");
    }

    public async Task RemoveRefreshToken()
    {
        await _localStorageService.RemoveItemAsync("refreshToken");
    }
}