using Repositories;
using Utilities;

namespace Services.Administration;

public class AdministrationService : IAdministrationService
{
    private readonly UnitOfWork _unitOfWork;

    public AdministrationService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool success, string message)> DeleteAccount(int accountId)
    {
        if (!_unitOfWork.Accounts.DoesIdExist(accountId)) return (false, Prompts.ACCOUNT_NOT_EXIST);
        
        var account = _unitOfWork.Accounts.GetById(accountId);

        _unitOfWork.Accounts.Remove(account);
        await _unitOfWork.CompleteAsync();

        return (true, "Account deleted successfully");
    }
}