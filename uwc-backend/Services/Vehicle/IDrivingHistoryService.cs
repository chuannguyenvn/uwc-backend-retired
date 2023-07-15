using Models;

namespace Services.Vehicle;

public interface IDrivingHistoryService
{
    public (bool success, object result) AddDrivingHistory(DateTime date, int driverId, int vehicleId);
    public List<DrivingHistory> GetAllDrivingHistory();
    public (bool success, object result) DeleteDrivingHistory(int id);
}