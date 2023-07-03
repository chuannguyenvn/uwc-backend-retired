using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repositories;

namespace Services.Authentication;

public interface IAuthenticationService
{
    public (bool success, string content) Register(string username, string password, int employeeId, string settings);

    public (bool success, string token) Login(string username, string password);

    public (bool success, object result) UpdatePassword(string username, string oldPassword, string newPassword);

    public (bool success, object result) DeleteAccount(string username, string password);

    public (bool success, object result) UpdateSettings(string username, string password, string settings);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly Settings _settings;
    private readonly UnitOfWork _unitOfWork;

    public AuthenticationService(Settings settings, UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _settings = settings;
    }

    public (bool success, string content) Register(string username, string password, int employeeId, string settings)
    {
        if (_unitOfWork.Accounts.DoesUsernameExist(username))
            return (false, "Username has already been taken.");

        if (!_unitOfWork.Employees.DoesIdExist(employeeId))
            return (false, "Employee does not exist.");

        var accountList = _unitOfWork.Accounts.Find(account => account.Employee.Id == employeeId);
        if (accountList.Any()) return (false, "Employee already has an account.");

        var employee = _unitOfWork.Employees.Find(employee => employee.Id == employeeId).First();
        var accountInformation = new Account {Username = username, Password = password, Employee = employee, Settings = settings};

        _unitOfWork.Accounts.Add(accountInformation);
        _unitOfWork.Complete();

        return (true, "Registered successfully.");
    }

    public (bool success, string token) Login(string username, string password)
    {
        if (!_unitOfWork.Accounts.ContainsUnique(account => account.Username == username, out var error))
            return (false, error);

        var account = _unitOfWork.Accounts.GetUnique(account => account.Username == username);
        if (account.Password != AuthenticationHelpers.ComputeHash(password, account.Salt)) return (false, "Password is incorrect.");
        return (true, GenerateJwtToken(AssembleClaimsIdentity(account)));
    }

    public (bool success, object result) UpdatePassword(string username, string oldPassword, string newPassword)
    {
        if (!_unitOfWork.Accounts.ContainsUnique(account => account.Username == username, out var error))
            return (false, error);

        var account = _unitOfWork.Accounts.GetUnique(account => account.Username == username);
        if (account.Password != oldPassword) return (false, "Incorrect old password.");

        account.Password = newPassword;
        _unitOfWork.Complete();
        return (true, "Update password successfully.");
    }

    public (bool success, object result) DeleteAccount(string username, string password)
    {
        if (!_unitOfWork.Accounts.ContainsUnique(account => account.Username == username, out var error))
            return (false, error);

        var account = _unitOfWork.Accounts.GetUnique(account => account.Username == username);
        if (account.Password != password) return (false, "Incorrect password.");

        _unitOfWork.Accounts.Remove(account);
        _unitOfWork.Complete();
        return (true, "Account deleted successfully");
    }

    public (bool success, object result) UpdateSettings(string username, string password, string settings)
    {
        if (!_unitOfWork.Accounts.ContainsUnique(account => account.Username == username, out var error))
            return (false, error);

        var account = _unitOfWork.Accounts.GetUnique(account => account.Username == username);
        if (account.Password != password) return (false, "Incorrect password.");

        account.Settings = settings;
        _unitOfWork.Complete();
        return (true, "Settings updated successfully.");
    }

    private ClaimsIdentity AssembleClaimsIdentity(Account account)
    {
        var subject = new ClaimsIdentity(new[] {new Claim("id", account.Employee.Id.ToString())});
        return subject;
    }

    private string GenerateJwtToken(ClaimsIdentity subject)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_settings.BearerKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

public static class AuthenticationHelpers
{
    public static void ProvideSaltAndHash(this Account account)
    {
        var salt = GenerateSalt();
        account.Salt = Convert.ToBase64String(salt);
        account.Password = ComputeHash(account.Password, account.Salt);
    }

    private static byte[] GenerateSalt()
    {
        var rng = RandomNumberGenerator.Create();
        var salt = new byte[24];
        rng.GetBytes(salt);
        return salt;
    }

    public static string ComputeHash(string password, string saltString)
    {
        var salt = Convert.FromBase64String(saltString);

        using var hashGenerator = new Rfc2898DeriveBytes(password, salt, 10101);
        var bytes = hashGenerator.GetBytes(24);
        return Convert.ToBase64String(bytes);
    }
}