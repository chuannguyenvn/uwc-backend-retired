using Commons.Types;
using Models;
using Repositories;
using Services.LiveData;

namespace Services.TaskEntry;

public interface IWorkService
{
    public (bool success, object result) AddTask(DateTime date, int supervisor, int worker, List<int> mcpIds);

    public List<Models.TaskEntry> GetAllTasksOfEmployee(int id);

    public (bool success, object result) DeleteAllTasksOfEmployee(int id);

    public (bool success, object result) DeleteTask(int id);

    public (bool success, object result) UpdateTask(int id, DateTime date, int supervisorId, int workerId, int routeId);

    public List<UserProfile> GetFreeEmployees();
}

public class WorkService : IWorkService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly VehicleLocationService _vehicleLocationService;

    public WorkService(UnitOfWork unitOfWork, VehicleLocationService vehicleLocationService)
    {
        _unitOfWork = unitOfWork;
        _vehicleLocationService = vehicleLocationService;
    }

    public (bool success, object result) AddTask(DateTime date, int supervisor, int worker, List<int> mcpIds)
    {
        if (!_unitOfWork.SupervisorProfiles.DoesIdExist(supervisor))
            return (false, "Supervisor Id does not exist");
        
        if (!_unitOfWork.DriverProfiles.DoesIdExist(worker)) return (false, "Worker Id does not exist.");

        var supervisorEmployee = _unitOfWork.SupervisorProfiles.Find(employee => employee.Id == supervisor).First();
        var workerEmployee = _unitOfWork.DriverProfiles.Find(employee => employee.Id == worker).First();

        if (supervisorEmployee.Role != UserRole.Supervisor) return (false, "Supervisor Id does not match");

        if (workerEmployee.Role == 0) return (false, "Worker Id does not match");

        var taskInformation = new Models.TaskEntry {Date = date, Supervisor = supervisorEmployee, Worker = workerEmployee};

        _unitOfWork.TaskEntries.Add(taskInformation);
        _unitOfWork.Complete();
        
        _vehicleLocationService.AddRoute(workerEmployee.Id - 10, mcpIds);
        
        return (true, "Task added successfully");
    }

    public List<Models.TaskEntry> GetAllTasksOfEmployee(int id)
    {
        if (!_unitOfWork.DriverProfiles.DoesIdExist(id)) return new List<Models.TaskEntry>();

        var result = _unitOfWork.TaskEntries.Find(task => task.Worker.Id == id);
        return result.OrderBy(task => task.Date).ToList();
    }

    public (bool success, object result) DeleteAllTasksOfEmployee(int id)
    {
        if (!_unitOfWork.DriverProfiles.DoesIdExist(id)) return (false, "Employee Id does not exist.");

        var taskList = _unitOfWork.TaskEntries.Find(task => task.Worker.Id == id);
        _unitOfWork.TaskEntries.RemoveRange(taskList);
        _unitOfWork.Complete();

        return (true, "Delete all tasks of an employee successfully.");
    }

    public (bool success, object result) DeleteTask(int id)
    {
        if (!_unitOfWork.TaskEntries.DoesIdExist(id)) return (false, "Task Id does not exist.");

        var task = _unitOfWork.TaskEntries.Find(task => task.Id == id).First();
        _unitOfWork.TaskEntries.Remove(task);
        _unitOfWork.Complete();

        return (true, "Task removed successfully");
    }

    public (bool success, object result) UpdateTask(int id, DateTime date, int supervisorId, int workerId, int routeId)
    {
        if (!_unitOfWork.TaskEntries.DoesIdExist(id)) return (false, "Task Id does not exist.");

        if (!_unitOfWork.DriverProfiles.DoesIdExist(supervisorId))
            return (false, "Supervisor Id does not exist.");

        if (!_unitOfWork.DriverProfiles.DoesIdExist(workerId))
            return (false, "Worker Id does not exist.");

        var supervisor = _unitOfWork.SupervisorProfiles.Find(supervisor => supervisor.Id == supervisorId).First();
        var worker = _unitOfWork.DriverProfiles.Find(worker => worker.Id == workerId).First();

        var task = _unitOfWork.TaskEntries.Find(task => task.Id == id).First();
        task.Supervisor = supervisor;
        task.Worker = worker;
        task.Date = date;

        _unitOfWork.Complete();
        return (true, "Task updated successfully");
    }

    public List<UserProfile> GetFreeEmployees()
    {
        var taskList = _unitOfWork.TaskEntries.Find(task => task.Date >= DateTime.Now && task.Date <= DateTime.Now.AddHours(24));
        IEnumerable<UserProfile> freeDrivers =
            _unitOfWork.DriverProfiles.Find(driverProfile => taskList.All(task => task.Worker.Id != driverProfile.Id));
        IEnumerable<UserProfile> freeCleaners =
            _unitOfWork.CleanerProfiles.Find(cleanerProfile => taskList.All(task => task.Worker.Id != cleanerProfile.Id));

        return freeDrivers.Concat(freeCleaners).ToList();
    }
}