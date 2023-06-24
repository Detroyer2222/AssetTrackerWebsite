using AssetTrackerWebsite.HttpExtensions;
using AssetTrackerWebsite.Models.Authentication.Login;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AssetTrackerWebsite.Pages.Login;

public partial class LoginDialog : ComponentBase
{


    [CascadingParameter]
    MudDialogInstance MudDialog { get; set; }

    [Inject]
    ISnackbar Snackbar { get; set; }

    [Inject]
    AssetTrackerApiHttpClient Http { get; set; }

    UserLoginDto LoginModel { get; set; } = new UserLoginDto();
    public bool FormValid { get; set; } = false;
    public bool FormInvalid => !FormValid;
    

    private async Task Login()
    {
        var loginResult = await Http.LoginUser(LoginModel);
        if (!String.IsNullOrEmpty(loginResult.AccessToken))
        {
            Snackbar.Add("Login successful", Severity.Success);
            MudDialog.Close(DialogResult.Ok(true));
        }
        else
        {
            Snackbar.Add("Login failed", Severity.Error);
        }
    }

    private void ChangeRegister()
    {
        MudDialog.Close(DialogResult.Ok("Register"));
    }
}