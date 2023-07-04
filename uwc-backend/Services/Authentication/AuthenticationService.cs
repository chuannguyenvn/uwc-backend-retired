using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repositories;

namespace Services.Authentication;

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

    private ClaimsIdentity AssembleClaimsIdentity(Account account)
    {
        var subject = new ClaimsIdentity(new[]
        {
            new Claim("id", account.Employee.Id.ToString()), new Claim("role", account.Employee.Role.ToString())
        });
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