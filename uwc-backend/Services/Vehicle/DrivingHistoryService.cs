using Models;
using Repositories;
using Commons.Types;

namespace Services.Vehicle;

public class DrivingHistoryService : IDrivingHistoryService
{
    private readonly UnitOfWork _unitOfWork;

    public DrivingHistoryService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public (bool success, object result) AddDrivingHistory(DateTime date, int driverId, int vehicleId)
    {
        if (!_unitOfWork.DriverProfiles.DoesIdExist(driverId))
            return (false, "Employee Id does not exist.");

        if (!_unitOfWork.Vehicles.DoesIdExist(vehicleId))
            return (false, "Vehicle Id does not exist.");

        var driverList = _unitOfWork.DriverProfiles.Find(driver => driver.Id == driverId && driver.Role == UserRole.Driver).ToList();
        if (!driverList.Any()) return (false, "Wrong role.");

        var driver = driverList.First();
        var vehicle = _unitOfWork.Vehicles.Find(vehicle => vehicle.Id == vehicleId).First();

        var driveInformation = new DrivingHistory {Timestamp = date, DriverProfile = driver, VehicleUsed = vehicle};
        _unitOfWork.DrivingHistories.Add(driveInformation);
        _unitOfWork.Complete();

        return (true, "Add driving history successfully.");
    }

    public List<DrivingHistory> GetAllDrivingHistory()
    {
        var drivingList = _unitOfWork.DrivingHistories.GetAll();
        return drivingList.ToList();
    }

    public (bool success, object result) DeleteDrivingHistory(int id)
    {
        if (!_unitOfWork.DrivingHistories.DoesIdExist(id))
            return (false, "Driving history Id does not exist.");

        var drivingHistory = _unitOfWork.DrivingHistories.Find(drive => drive.Id == id).First();

        _unitOfWork.DrivingHistories.Remove(drivingHistory);
        _unitOfWork.Complete();
        return (true, "Driving history deleted successfully");
    }
}