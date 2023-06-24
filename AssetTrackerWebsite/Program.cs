using AssetTrackerWebsite;
using AssetTrackerWebsite.HttpExtensions;
using AssetTrackerWebsite.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddBlazoredLocalStorage(options =>
{
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddHttpClient<AssetTrackerApiHttpClient>(client =>
    client.BaseAddress = new Uri("https://localhost:7076"));
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());

await builder.Build().RunAsync();
