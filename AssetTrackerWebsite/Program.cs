using AssetTrackerWebsite;
using AssetTrackerWebsite.HttpExtensions;
using AssetTrackerWebsite.Services;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});
builder.Services.AddBlazoredSessionStorage(options =>
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true
);

builder.Services.AddHttpClient<AssetTrackerApiHttpClient>("AssetTrackerApi", client =>
    client.BaseAddress = new Uri("https://localhost:7076"));
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AssetTrackerAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<AssetTrackerAuthenticationStateProvider>());


await builder.Build().RunAsync();
