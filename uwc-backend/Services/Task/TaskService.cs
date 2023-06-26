using Repositories;

namespace Services.Task;

public interface ITaskService
{
    public (bool success, object result) AddTask(DateTime date, int supervisor, int worker, int route);
    public List<Models.Task> GetTasksOfEmployee(int id);
    public (bool success, object result) DeleteTasksOfEmployee(int id);
    public (bool success, object result) DeleteTask(int id);
}

public class TaskService : ITaskService
{
    private readonly UnitOfWork _unitOfWork;

    public TaskService(UnitOfWork _unitOfWork)
    {
        this._unitOfWork = _unitOfWork;
    }


    public (bool success, object result) AddTask(DateTime date, int supervisor, int worker, int route)
    {
        if (!_unitOfWork.Employees.DoesIdExist(supervisor))
        {
            return (false, "Supervisor Id does not exist");
        }

        if (!_unitOfWork.Employees.DoesIdExist(worker))
        {
            return (false, "Worker Id does not exist.");
        }

        if (!_unitOfWork.Routes.DoesIdExist(route))
        {
            return (false, "Route Id does not exist.");
        }

        var supervisorEmployee = _unitOfWork.Employees.Find(employee => employee.Id == supervisor).First();
        var workerEmployee = _unitOfWork.Employees.Find(employee => employee.Id == worker).First();
        var routeTravel = _unitOfWork.Routes.Find(routing => routing.Id == route).First();

        if (supervisorEmployee.Role != 0)
        {
            return (false, "Supervisor Id does not match");
        }

        if (workerEmployee.Role == 0)
        {
            return (false, "Worker Id does not match");
        }
        
        var taskInformation = new Models.Task()
        {
            Date = date,
            Supervisor = supervisorEmployee,
            Worker = workerEmployee,
            Route = routeTravel,
        };
        
        _unitOfWork.Tasks.Add(taskInformation);
        _unitOfWork.Complete();
        return (true, "Task added successfully");
    }

    public List<Models.Task> GetTasksOfEmployee(int id)
    {
        if (!_unitOfWork.Employees.DoesIdExist(id))
        {
            return new List<Models.Task>();
        }

        var result = _unitOfWork.Tasks.Find(task => task.Worker.Id == id);
        return result.OrderBy(task => task.Date).ToList();
    }

    public (bool success, object result) DeleteTasksOfEmployee(int id)
    {
        if (!_unitOfWork.Employees.DoesIdExist(id))
        {
            return (false, "Employee Id does not exist.");
        }

        var taskList = _unitOfWork.Tasks.Find(task => task.Worker.Id == id);
        _unitOfWork.Tasks.RemoveRange(taskList);
        _unitOfWork.Complete();

        return (true, "Delete all tasks of an employee successfully.");
    }

    public (bool success, object result) DeleteTask(int id)
    {
        if (!_unitOfWork.Tasks.DoesIdExist(id))
        {
            return (false, "Task Id does not exist.");
        }

        var task = _unitOfWork.Tasks.Find(task => task.Id == id).First();
        _unitOfWork.Tasks.Remove(task);
        _unitOfWork.Complete();

        return (true, "Task removed successfully");
    }
}