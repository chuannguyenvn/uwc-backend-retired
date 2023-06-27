using Repositories;

namespace uwc_backend.Services.Vehicle;

public interface IVehicleService
{
    public (bool success, object result) AddVehicle(double capacity, double currentLoad, double averageSpeed);
}

public class VehicleService : IVehicleService
{
    private readonly UnitOfWork _unitOfWork;

    public VehicleService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public (bool success, object result) AddVehicle(double capacity, double currentLoad, double averageSpeed)
    {
        var vehicleInformation = new Models.Vehicle()
        {
            Capacity = capacity,
            CurrentLoad = currentLoad,
            AverageSpeed = averageSpeed,
        };
        
        _unitOfWork.Vehicles.Add(vehicleInformation);
        _unitOfWork.Complete();

        return (true, "Vehicle added successfully");
    }
}