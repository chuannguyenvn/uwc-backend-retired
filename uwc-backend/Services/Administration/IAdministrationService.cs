namespace Services.Administration;

public interface IAdministrationService
{
    public Task<(bool success, string message)> DeleteAccount(int accountId);
}