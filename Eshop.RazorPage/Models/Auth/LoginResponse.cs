namespace Eshop.RazorPage.Models.Auth;

public class LoginResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}

public class LoginCommand
{
    public string phoneNumber { get; set; }
    public string Password { get; set; }

}

public class RegisterCommand
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}