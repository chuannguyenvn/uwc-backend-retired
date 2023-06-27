using Repositories;

namespace uwc_backend.Services.Vehicle;

public interface IVehicleService
{
    
}

public class VehicleService : IVehicleService
{
    private readonly UnitOfWork _unitOfWork;

    public VehicleService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    
}