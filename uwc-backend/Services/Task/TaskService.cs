﻿using Repositories;

namespace Services.Task;

public interface ITaskService
{
    public (bool success, object result) AddTask(DateTime date, int supervisor, int worker, int route);

    public List<Models.Task> GetTasksOfEmployee(int id);

    public (bool success, object result) DeleteTasksOfEmployee(int id);

    public (bool success, object result) DeleteTask(int id);

    public (bool success, object result) UpdateTask(int id, DateTime date, int supervisorId, int workerId, int routeId);

    public List<Models.Task> GetTasksInTimeRange(DateTime startTime, DateTime endTime);

    public List<Models.Employee> GetFreeEmployees();

    public List<Models.Employee> SortByTasksDescendingly();

    public List<Models.Employee> SortByTasksAscendingly();
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
            return (false, "Supervisor Id does not exist");

        if (!_unitOfWork.Employees.DoesIdExist(worker)) return (false, "Worker Id does not exist.");

        if (!_unitOfWork.Routes.DoesIdExist(route)) return (false, "Route Id does not exist.");

        var supervisorEmployee = _unitOfWork.Employees.Find(employee => employee.Id == supervisor).First();
        var workerEmployee = _unitOfWork.Employees.Find(employee => employee.Id == worker).First();
        var routeTravel = _unitOfWork.Routes.Find(routing => routing.Id == route).First();

        if (supervisorEmployee.Role != 0) return (false, "Supervisor Id does not match");

        if (workerEmployee.Role == 0) return (false, "Worker Id does not match");

        var taskInformation = new Models.Task {Date = date, Supervisor = supervisorEmployee, Worker = workerEmployee, Route = routeTravel};

        _unitOfWork.Tasks.Add(taskInformation);
        _unitOfWork.Complete();
        return (true, "Task added successfully");
    }

    public List<Models.Task> GetTasksOfEmployee(int id)
    {
        if (!_unitOfWork.Employees.DoesIdExist(id)) return new List<Models.Task>();

        var result = _unitOfWork.Tasks.Find(task => task.Worker.Id == id);
        return result.OrderBy(task => task.Date).ToList();
    }

    public (bool success, object result) DeleteTasksOfEmployee(int id)
    {
        if (!_unitOfWork.Employees.DoesIdExist(id)) return (false, "Employee Id does not exist.");

        var taskList = _unitOfWork.Tasks.Find(task => task.Worker.Id == id);
        _unitOfWork.Tasks.RemoveRange(taskList);
        _unitOfWork.Complete();

        return (true, "Delete all tasks of an employee successfully.");
    }

    public (bool success, object result) DeleteTask(int id)
    {
        if (!_unitOfWork.Tasks.DoesIdExist(id)) return (false, "Task Id does not exist.");

        var task = _unitOfWork.Tasks.Find(task => task.Id == id).First();
        _unitOfWork.Tasks.Remove(task);
        _unitOfWork.Complete();

        return (true, "Task removed successfully");
    }

    public (bool success, object result) UpdateTask(int id, DateTime date, int supervisorId, int workerId, int routeId)
    {
        if (!_unitOfWork.Tasks.DoesIdExist(id)) return (false, "Task Id does not exist.");

        if (!_unitOfWork.Employees.DoesIdExist(supervisorId))
            return (false, "Supervisor Id does not exist.");

        if (!_unitOfWork.Employees.DoesIdExist(workerId))
            return (false, "Worker Id does not exist.");

        if (!_unitOfWork.Routes.DoesIdExist(routeId)) return (false, "Route Id does not exist.");

        var supervisor = _unitOfWork.Employees.Find(supervisor => supervisor.Id == supervisorId).First();
        var worker = _unitOfWork.Employees.Find(worker => worker.Id == workerId).First();
        var route = _unitOfWork.Routes.Find(route => route.Id == routeId).First();

        var task = _unitOfWork.Tasks.Find(task => task.Id == id).First();
        task.Supervisor = supervisor;
        task.Worker = worker;
        task.Route = route;
        task.Date = date;

        _unitOfWork.Complete();
        return (true, "Task updated successfully");
    }

    public List<Models.Task> GetTasksInTimeRange(DateTime startTime, DateTime endTime)
    {
        var taskList = _unitOfWork.Tasks.Find(task => task.Date >= startTime && task.Date <= endTime);
        return taskList.ToList();
    }

    public List<Models.Employee> GetFreeEmployees()
    {
        var taskList = _unitOfWork.Tasks.Find(task => task.Date >= DateTime.Now && task.Date <= DateTime.Now.AddHours(24));
        var employeeList = _unitOfWork.Employees.Find(employee =>
            employee.Role != 2 && taskList.All(task => task.Worker.Id != employee.Id));

        return employeeList.ToList();
    }

    public List<Models.Employee> SortByTasksDescendingly()
    {
        var employeeList = _unitOfWork.Employees.GetAll()
            .Where(employee => employee.Role != 0)
            .OrderBy(employee => _unitOfWork.Tasks.Count(task => task.Worker.Id == employee.Id));

        return employeeList.ToList();
    }

    public List<Models.Employee> SortByTasksAscendingly()
    {
        var employeeList = _unitOfWork.Employees.GetAll()
            .Where(employee => employee.Role != 0)
            .OrderByDescending(employee => _unitOfWork.Tasks.Count(task => task.Worker.Id == employee.Id));

        return employeeList.ToList();
    }
}