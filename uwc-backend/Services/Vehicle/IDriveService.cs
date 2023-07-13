using Models;

namespace Services.Vehicle;

public interface IDriveService
{
    public (bool success, object result) AddDrive(DateTime date, int driverId, int vehicleId);
    public List<DrivingHistory> GetAllDrives();
    public (bool success, object result) DeleteDrive(int id);
    public List<DrivingHistory> GetDriverFullVehicleSortByName();
    public List<DrivingHistory> GetDriverFullVehicleSortByCurrentLoad();
}