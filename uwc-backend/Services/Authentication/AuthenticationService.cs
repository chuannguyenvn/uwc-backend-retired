using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repositories;
using Utilities;

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

    public async Task<(bool success, string message)> Register(string username, string password, int employeeId, string settings)
    {
        if (_unitOfWork.Accounts.DoesUsernameExist(username))
            return (false, "Username has already been taken.");

        if (!_unitOfWork.Employees.DoesIdExist(employeeId))
            return (false, Prompts.EMPLOYEE_NOT_EXIST);

        var accountList = _unitOfWork.Accounts.Find(account => account.Employee.Id == employeeId);
        if (accountList.Any()) return (false, "Employee already has an account.");

        var employee = _unitOfWork.Employees.Find(employee => employee.Id == employeeId).First();
        var accountInformation = new Account {Username = username, PasswordHash = password, Employee = employee, Settings = settings};

        _unitOfWork.Accounts.Add(accountInformation);
        await _unitOfWork.CompleteAsync();

        return (true, Prompts.SUCCESS);
    }

    public async Task<(bool success, string message, string token)> Login(string username, string password)
    {
        if (!_unitOfWork.Accounts.ContainsUnique(account => account.Username == username, out var error))
            return (false, error, "");

        var account = _unitOfWork.Accounts.GetUnique(account => account.Username == username);
        if (account.PasswordHash != password) return (false, Prompts.WRONG_PASSWORD, "");

        return (true, Prompts.SUCCESS, "token");
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