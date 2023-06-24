using AssetTrackerWebsite.HttpExtensions;
using AssetTrackerWebsite.Models.Authentication.Signup;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AssetTrackerWebsite.Pages.Login;

public partial class RegisterDialog : ComponentBase
{
    private MudTextField<string> passwordField;

    [CascadingParameter]
    MudDialogInstance MudDialog { get; set; }
        
    [Inject]
    ISnackbar Snackbar { get; set; }

    [Inject]
    AssetTrackerApiHttpClient Http { get; set; }

    UserSignupDto SignupModel { get; set; } = new UserSignupDto();
    public bool FormValid { get; set; } = false;
    public bool FormInvalid => !FormValid;

    private async Task Register()
    {
        var registerResult = await Http.RegisterUser(SignupModel);
        if (!String.IsNullOrEmpty(registerResult.Email))
        {
            Snackbar.Add("Registration successful", Severity.Success);
            MudDialog.Close(DialogResult.Ok(true));
        }
        else
        {
            Snackbar.Add("Registration failed", Severity.Error);
        }
    }

    private void ChangeLogin()
    {
        MudDialog.Close(DialogResult.Ok("Login"));
    }
    private string PasswordMatch(string arg)
    {
        if (passwordField.Value != arg)
            return "Passwords don't match";
        return null;
    }
}