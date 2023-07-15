namespace Services.Authentication;

public interface IAuthenticationService
{
    public Task<(bool success, string message)> Register(string username, string password, int employeeId);

    public Task<(bool success, string message, string token)> Login(string username, string password);
}