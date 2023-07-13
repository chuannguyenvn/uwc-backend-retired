using Models;
using Models.Types;
using Repositories;

namespace Services.Vehicle;

public class DriveService : IDriveService
{
    private readonly UnitOfWork _unitOfWork;

    public DriveService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public (bool success, object result) AddDrivingHistory(DateTime date, int driverId, int vehicleId)
    {
        if (!_unitOfWork.EmployeeProfiles.DoesIdExist(driverId))
            return (false, "Employee Id does not exist.");

        if (!_unitOfWork.Vehicles.DoesIdExist(vehicleId))
            return (false, "Vehicle Id does not exist.");

        var driverList = _unitOfWork.EmployeeProfiles.Find(driver => driver.Id == driverId && driver.Role == EmployeeRole.Driver).ToList();
        if (!driverList.Any()) return (false, "Wrong role.");

        var driver = driverList.First();
        var vehicle = _unitOfWork.Vehicles.Find(vehicle => vehicle.Id == vehicleId).First();

        var driveInformation = new DrivingHistory {Timestamp = date, Driver = driver, Vehicle = vehicle};
        _unitOfWork.Drives.Add(driveInformation);
        _unitOfWork.Complete();

        return (true, "Add driving history successfully.");
    }

    public List<DrivingHistory> GetAllDrivingHistory()
    {
        var drivingList = _unitOfWork.Drives.GetAll();
        return drivingList.ToList();
    }

    public (bool success, object result) DeleteDrivingHistory(int id)
    {
        if (!_unitOfWork.Drives.DoesIdExist(id))
            return (false, "Driving history Id does not exist.");

        var drivingHistory = _unitOfWork.Drives.Find(drive => drive.Id == id).First();

        _unitOfWork.Drives.Remove(drivingHistory);
        _unitOfWork.Complete();
        return (true, "Driving history deleted successfully");
    }
}