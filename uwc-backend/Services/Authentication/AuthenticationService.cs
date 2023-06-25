using Models;
using Repositories;

namespace Services.Authentication;

public interface IAuthenticationService
{
    public (bool success, string content) Register(string username, string password);
    public Task<(bool success, string token)> Login(string username, string password);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly UnitOfWork _unitOfWork;

    public AuthenticationService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public (bool success, string content) Register(string username, string password)
    {
        if (_unitOfWork.Accounts.DoesAccountWithUsernameExist(username))
        {
            return (false, "Username has already been taken.");
        }

        var accountInformation = new Account()
        {
            Username = username,
            Password = password,
        };

        _unitOfWork.Accounts.Add(accountInformation);
        _unitOfWork.Complete();

        return (true, "Registered successfully.");
    }

    public async Task<(bool success, string token)> Login(string username, string password)
    {
        var userList = _unitOfWork.Accounts.Find(account => account.Username == username);
        if (userList.Count() == 0)
        {
            return (false, "Account does not exist.");
        }

        if (userList.First().Password != password)
        {
            return (false, "Password is incorrect");
        }

        return (true, "Login successfully.");
    }
}