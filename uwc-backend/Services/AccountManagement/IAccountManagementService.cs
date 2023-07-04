namespace Services.AccountManagement;

public interface IAccountManagementService
{
    public Task<(bool success, string message)> UpdatePassword(int accountId, string oldPassword, string newPassword);

    public Task<(bool success, string message)> UpdateSettings(int accountId, string newSettings);
}