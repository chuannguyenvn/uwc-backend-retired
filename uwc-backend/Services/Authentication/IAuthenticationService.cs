namespace Services.Authentication;

public interface IAuthenticationService
{
    public (bool success, string content) Register(string username, string password, int employeeId, string settings);

    public (bool success, string token) Login(string username, string password);
}