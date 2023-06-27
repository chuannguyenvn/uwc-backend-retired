using Models;
using Repositories;

namespace uwc_backend.Services.Vehicle;

public interface IDriveService
{
    public (bool success, object result) AddDrive(DateTime date, int driverId, int vehicleId);

    public List<Models.Drive> GetAllDrives();
    public (bool success, object result) DeleteDrive(int id);
}

public class DriveService : IDriveService
{
    private readonly UnitOfWork _unitOfWork;

    public DriveService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public (bool success, object result) AddDrive(DateTime date, int driverId, int vehicleId)
    {
        if (!_unitOfWork.Employees.DoesIdExist(driverId))
        {
            return (false, "Employee Id does not exist.");
        }

        if (!_unitOfWork.Vehicles.DoesIdExist(vehicleId))
        {
            return (false, "Vehicle Id does not exist.");
        }

        var driverList = _unitOfWork.Employees.Find(driver => driver.Id == driverId && driver.Role == 1);
        if (driverList.Count() == 0)
        {
            return (false, "Wrong role.");
        }

        var driver = driverList.First();
        var vehicle = _unitOfWork.Vehicles.Find(vehicle => vehicle.Id == vehicleId).First();
        
        var driveInformation = new Drive()
        {
            Date = date,
            Driver = driver,
            Vehicle = vehicle,
        };
        _unitOfWork.Drives.Add(driveInformation);
        _unitOfWork.Complete();

        return (true, "Add driving history successfully.");
    }

    public List<Drive> GetAllDrives()
    {
        var drivingList = _unitOfWork.Drives.GetAll();
        return drivingList.ToList();
    }

    public (bool success, object result) DeleteDrive(int id)
    {
        if (!_unitOfWork.Drives.DoesIdExist(id))
        {
            return (false, "Driving history Id does not exist.");
        }

        var drivingHistory = _unitOfWork.Drives.Find(drive => drive.Id == id).First();
        
        _unitOfWork.Drives.Remove(drivingHistory);
        _unitOfWork.Complete();
        return (true, "Driving history deleted successfully");
    }
}