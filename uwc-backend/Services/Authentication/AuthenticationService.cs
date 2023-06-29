using Models;
using Repositories;
using Repositories.Implementations;

namespace Services.Authentication;

public interface IAuthenticationService
{
    public (bool success, string content) Register(string username, string password, int employeeId,
        string settings);

    public (bool success, string token) Login(string username, string password);

    public (bool success, object result) UpdatePassword(string username, string oldPassword,
        string newPassword);

    public (bool success, object result) DeleteAccount(string username, string password);

    public (bool success, object result) UpdateSettings(string username, string password,
        string settings);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly UnitOfWork _unitOfWork;
    
    public AuthenticationService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public (bool success, string content) Register(string username, string password, int employeeId,
        string settings)
    {
        if (_unitOfWork.Accounts.DoesUsernameExist(username))
            return (false, "Username has already been taken.");

        if (!_unitOfWork.Employees.DoesIdExist(employeeId))
            return (false, "Employee does not exist.");

        var accountList = _unitOfWork.Accounts.Find(account => account.Employee.Id == employeeId);
        if (accountList.Any()) return (false, "Employee already has an account.");

        var employee = _unitOfWork.Employees.Find(employee => employee.Id == employeeId).First();
        var accountInformation = new Account
        {
            Username = username, Password = password, Employee = employee, Settings = settings
        };

        _unitOfWork.Accounts.Add(accountInformation);
        _unitOfWork.Complete();

        return (true, "Registered successfully.");
    }

    public (bool success, string token) Login(string username, string password)
    {
        if (!_unitOfWork.Accounts.ContainsUnique(account => account.Username == username,
                out string error))
            return (false, error);

        var account = _unitOfWork.Accounts.GetUnique(account => account.Username == username);
        if (account.Password != password) return (false, "Password is incorrect.");
        return (true, "Login successfully.");
    }

    public (bool success, object result) UpdatePassword(string username, string oldPassword,
        string newPassword)
    {
        if (!_unitOfWork.Accounts.ContainsUnique(account => account.Username == username,
                out string error))
            return (false, error);

        var account = _unitOfWork.Accounts.GetUnique(account => account.Username == username);
        if (account.Password != oldPassword) return (false, "Incorrect old password.");

        account.Password = newPassword;
        _unitOfWork.Complete();
        return (true, "Update password successfully.");
    }

    public (bool success, object result) DeleteAccount(string username, string password)
    {
        if (!_unitOfWork.Accounts.ContainsUnique(account => account.Username == username,
                out string error))
            return (false, error);

        var account = _unitOfWork.Accounts.GetUnique(account => account.Username == username);
        if (account.Password != password) return (false, "Incorrect password.");

        _unitOfWork.Accounts.Remove(account);
        _unitOfWork.Complete();
        return (true, "Account deleted successfully");
    }

    public (bool success, object result) UpdateSettings(string username, string password,
        string settings)
    {
        if (!_unitOfWork.Accounts.ContainsUnique(account => account.Username == username,
                out string error))
            return (false, error);

        var account = _unitOfWork.Accounts.GetUnique(account => account.Username == username);
        if (account.Password != password) return (false, "Incorrect password.");

        account.Settings = settings;
        _unitOfWork.Complete();
        return (true, "Settings updated successfully.");
    }
}