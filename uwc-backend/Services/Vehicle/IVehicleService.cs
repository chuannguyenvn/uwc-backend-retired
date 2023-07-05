namespace Services.Vehicle;

public interface IVehicleService
{
    public (bool success, object result) AddVehicle(double capacity, double currentLoad, double averageSpeed);

    public List<Models.Vehicle> GetAllVehicles();

    public (bool success, object result) UpdateVehicleInformation(int id, double capacity, double currentLoad, double averageSpeed);

    public (bool success, object result) DeleteVehicle(int id);
}