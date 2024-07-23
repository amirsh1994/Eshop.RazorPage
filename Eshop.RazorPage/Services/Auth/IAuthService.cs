namespace Eshop.RazorPage.Services.Auth;

public interface IAuthService
{
    
}


public class AuthService(HttpClient client,CancellationToken cancellationToken) : IAuthService
{
    public async Task RAdd()
    {
        await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "as"), cancellationToken);
    }
}