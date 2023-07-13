using Repositories;

namespace Services.Vehicle;

public class VehicleService : IVehicleService
{
    private readonly UnitOfWork _unitOfWork;

    public VehicleService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public (bool success, object result) AddVehicle(double capacity, double currentLoad, double averageSpeed)
    {
        var vehicleInformation = new Models.Vehicle {Capacity = capacity, CurrentLoad = currentLoad, AverageSpeed = averageSpeed};

        _unitOfWork.Vehicles.Add(vehicleInformation);
        _unitOfWork.Complete();

        return (true, "Vehicle added successfully");
    }

    public List<Models.Vehicle> GetAllVehicles()
    {
        var result = _unitOfWork.Vehicles.GetAll();
        return result.ToList();
    }

    public (bool success, object result) UpdateVehicle(int id, double capacity, double currentLoad, double averageSpeed)
    {
        if (!_unitOfWork.Vehicles.DoesIdExist(id)) return (false, "Vehicle Id does not exist.");

        var vehicle = _unitOfWork.Vehicles.Find(vehicle => vehicle.Id == id).First();
        vehicle.Capacity = capacity;
        vehicle.CurrentLoad = currentLoad;
        vehicle.AverageSpeed = averageSpeed;

        _unitOfWork.Complete();
        return (true, "Vehicle information updated successfully.");
    }

    public (bool success, object result) DeleteVehicle(int id)
    {
        if (!_unitOfWork.Vehicles.DoesIdExist(id)) return (false, "Vehicle Id does not exist.");

        var vehicle = _unitOfWork.Vehicles.Find(vehicle => vehicle.Id == id).First();
        _unitOfWork.Vehicles.Remove(vehicle);
        _unitOfWork.Complete();

        return (true, "Vehicle deleted successfully");
    }
}