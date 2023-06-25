using Models;
using Repositories;

namespace Services.Authentication;

public interface IAuthenticationService
{
    public (bool success, string content) Register(string username, string password, int employeeId);
    public Task<(bool success, string token)> Login(string username, string password);
    public (bool success, object result) UpdatePassword(string username, string oldPassword, string newPassword);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly UnitOfWork _unitOfWork;

    public AuthenticationService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public (bool success, string content) Register(string username, string password, int employeeId)
    {
        if (_unitOfWork.Accounts.DoesAccountWithUsernameExist(username))
        {
            return (false, "Username has already been taken.");
        }

        if (!_unitOfWork.Employees.DoesIdExist(employeeId))
        {
            return (false, "Employee does not exist");
        }

        var accountList = _unitOfWork.Accounts.Find(account => account.Employee.Id == employeeId);
        if (accountList.Any())
        {
            return (false, "Employee has account already.");
        }

        var employee = _unitOfWork.Employees.Find(employee => employee.Id == employeeId).First();
        var accountInformation = new Account()
        {
            Username = username,
            Password = password,
            Employee = employee,
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

    public (bool success, object result) UpdatePassword(string username, string oldPassword, string newPassword)
    {
        if (!_unitOfWork.Accounts.DoesAccountWithUsernameExist(username))
        {
            return (false, "Account does not exist.");
        }

        var account = _unitOfWork.Accounts.Find(account => account.Username == username).First();
        if (account.Password != oldPassword)
        {
            return (false, "Incorrect old password.");
        }

        account.Password = newPassword;
        _unitOfWork.Complete();
        return (true, "Update password successfully");
    }
}