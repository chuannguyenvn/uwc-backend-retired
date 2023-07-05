using Repositories;
using Utilities;

namespace Services.AccountManagement;

public class AccountManagementService : IAccountManagementService
{
    private readonly UnitOfWork _unitOfWork;

    public AccountManagementService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool success, string message)> UpdatePassword(int accountId, string oldPassword, string newPassword)
    {
        var account = _unitOfWork.Accounts.GetById(accountId);
        if (account.Password != oldPassword) return new ValueTuple<bool, string>(false, Prompts.WRONG_PASSWORD);

        account.Password = newPassword;
        try
        {
            await _unitOfWork.CompleteAsync();
        }
        catch
        {
            return (false, Prompts.SETTINGS_UPDATE_FAIL);
        }

        return (true, Prompts.SUCCESS);
    }

    public async Task<(bool success, string message)> UpdateSettings(int accountId, string newSettings)
    {
        var account = _unitOfWork.Accounts.GetById(accountId);

        account.Settings = newSettings;
        try
        {
            await _unitOfWork.CompleteAsync();
        }
        catch
        {
            return (false, Prompts.SETTINGS_UPDATE_FAIL);
        }

        return (true, Prompts.SUCCESS);
    }
}