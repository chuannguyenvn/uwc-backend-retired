using Repositories;

namespace uwc_backend.Services.Vehicle;

public interface IDrivingLicenseService
{
    
}

public class DrivingLicenseService : IDrivingLicenseService
{
    private readonly UnitOfWork _unitOfWork;

    public DrivingLicenseService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}