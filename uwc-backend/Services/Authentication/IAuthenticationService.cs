namespace Services.Authentication;

public interface IAuthenticationService
{
    public (bool success, string content) Register(string username, string password, int employeeId, string settings);

    public (bool success, string token) Login(string username, string password);

    public (bool success, object result) UpdatePassword(string username, string oldPassword, string newPassword);

    public (bool success, object result) DeleteAccount(int id);

    public (bool success, object result) UpdateSettings(string username, string password, string settings);
}