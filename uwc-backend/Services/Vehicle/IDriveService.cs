using Models;

namespace Services.Vehicle;

public interface IDriveService
{
    public (bool success, object result) AddDrive(DateTime date, int driverId, int vehicleId);
    public List<Drive> GetAllDrives();
    public (bool success, object result) DeleteDrive(int id);
    public List<Drive> GetDriverFullVehicleSortByName();
    public List<Drive> GetDriverFullVehicleSortByCurrentLoad();
}