using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using AssetTrackerWebsite.Models.Authentication.Login;
using AssetTrackerWebsite.Models.Authentication.Signup;
using AssetTrackerWebsite.Services;

namespace AssetTrackerWebsite.HttpExtensions;

public class AssetTrackerApiHttpClient
{
    private readonly ILogger<AssetTrackerApiHttpClient> _logger;
    private readonly HttpClient _http;
    private readonly ITokenService _tokenService;
    private readonly AssetTrackerAuthenticationStateProvider _authenticationStateProvider;

    public AssetTrackerApiHttpClient(ILogger<AssetTrackerApiHttpClient> logger, HttpClient http, ITokenService tokenService, AssetTrackerAuthenticationStateProvider authenticationStateProvider)
    {
        _logger = logger;
        _http = http;
        _tokenService = tokenService;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<UserSignupResponseDto> RegisterUser(UserSignupDto userRegisterDTO)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/authentication/signup", userRegisterDTO);
            var result = await response.Content.ReadFromJsonAsync<UserSignupResponseDto>();
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<UserLoginResultDto> LoginUser(UserLoginDto userLoginDTO)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/authentication/login", userLoginDTO);
            var result = await response.Content.ReadFromJsonAsync<UserLoginResultDto>();
            var validTo = new JwtSecurityTokenHandler().ReadJwtToken(result.AccessToken).ValidTo;

            // Token invalid
            if (DateTime.Compare(validTo, DateTime.UtcNow) <= 0)
            {
                return null;
            }

            await _tokenService.SetJwtToken(result.AccessToken, validTo);
            await _tokenService.SetRefreshToken(result.RefreshToken, DateTime.Now.AddDays(7));

            _authenticationStateProvider.StateChanged();
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return null;
        }
    }
}