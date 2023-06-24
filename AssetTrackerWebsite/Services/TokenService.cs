using AssetTrackerWebsite.Models.Authentication;
using AssetTrackerWebsite.Models.Authentication.Login;
using Blazored.LocalStorage;

namespace AssetTrackerWebsite.Services;

public interface ITokenService
{
    Task<TokenStore> GetToken();
    Task RemoveToken();
    Task SetToken(UserLoginResultDto tokenDTO);
}

public class TokenService : ITokenService
{
    private readonly ILocalStorageService localStorageService;

    public TokenService(ILocalStorageService localStorageService)
    {
        this.localStorageService = localStorageService;
    }

    public async Task SetToken(UserLoginResultDto tokenDTO)
    {
        //TODO - Add expiration date to token
        await localStorageService.SetItemAsync("token", new TokenStore
        {
            AccessToken = tokenDTO.AccessToken,
            RefreshToken = tokenDTO.RefreshToken,
            AccessTokenExpiration = DateTime.Now.AddMinutes(60),
            //RefreshTokenExpiry = DateTime.Now.AddSeconds(tokenDTO.RefreshTokenExpiresIn)
        });
    }

    public async Task<TokenStore> GetToken()
    {
        return await localStorageService.GetItemAsync<TokenStore>("token");
    }

    public async Task RemoveToken()
    {
        await localStorageService.RemoveItemAsync("token");
    }
}