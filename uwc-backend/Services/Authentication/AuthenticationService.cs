using Repositories;

namespace Services.Authentication;

public interface IAuthenticationService
{
    public (bool success, string content) Register(string username, string password);
    public (bool success, string token) Login(string username, string password);
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
        
        return (true, "");
    }

    public (bool success, string token) Login(string username, string password)
    {
        return (true, "");
    }
}