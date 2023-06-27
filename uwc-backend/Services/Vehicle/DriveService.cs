using Repositories;

namespace uwc_backend.Services.Vehicle;

public interface IDriveService
{
    
}

public class DriveService : IVehicleService
{
    private readonly UnitOfWork _unitOfWork;

    public DriveService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}