using Models;
using Repositories;

namespace uwc_backend.Services.Vehicle;

public interface IDrivingLicenseService
{
    public (bool success, object result) AddDrivingLicense(DateTime issueDate, string issuePlace, int ownerId, string type);

    public List<DrivingLicense> GetDrivingLicenseDriver(int id);

    public (bool success, object result) UpdateDrivingLicenseInformation(int id, DateTime issueDate, string issuePlace, int ownerId,
        string type);

    public (bool success, object result) DeleteDrivingLicense(int id);

    public (bool success, object result) DeleteOutdatedDrivingLicense();
}

public class DrivingLicenseService : IDrivingLicenseService
{
    private readonly UnitOfWork _unitOfWork;

    public DrivingLicenseService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public (bool success, object result) AddDrivingLicense(DateTime issueDate, string issuePlace, int ownerId, string type)
    {
        if (!_unitOfWork.Employees.DoesIdExist(ownerId))
            return (false, "Employee Id does not exist.");

        var owner = _unitOfWork.Employees.Find(employee => employee.Id == ownerId).First();

        if (owner.Role != 1) return (false, "Employee Id is not a driver");

        var drivingLicenseInformation = new DrivingLicense {IssueDate = issueDate, IssuePlace = issuePlace, Owner = owner, Type = type};

        _unitOfWork.DrivingLicenses.Add(drivingLicenseInformation);
        _unitOfWork.Complete();

        return (true, "Driving license added successfully");
    }

    public List<DrivingLicense> GetDrivingLicenseDriver(int id)
    {
        if (!_unitOfWork.Employees.DoesIdExist(id)) return new List<DrivingLicense>();

        var drivingLicenseList = _unitOfWork.DrivingLicenses.Find(dl => dl.Owner.Id == id);
        return drivingLicenseList.ToList();
    }

    public (bool success, object result) UpdateDrivingLicenseInformation(int id, DateTime issueDate, string issuePlace, int ownerId,
        string type)
    {
        if (!_unitOfWork.DrivingLicenses.DoesIdExist(id))
            return (false, "Driving license Id does not exist.");

        var drivingLicense = _unitOfWork.DrivingLicenses.Find(dl => dl.Id == id).First();
        var owner = _unitOfWork.Employees.Find(employee => employee.Id == ownerId).First();

        if (owner.Role != 1) return (false, "Owner is not a driver");

        drivingLicense.IssueDate = issueDate;
        drivingLicense.IssuePlace = issuePlace;
        drivingLicense.Owner = owner;
        drivingLicense.Type = type;

        _unitOfWork.Complete();
        return (true, "Driving license updated successfully");
    }

    public (bool success, object result) DeleteDrivingLicense(int id)
    {
        if (!_unitOfWork.DrivingLicenses.DoesIdExist(id))
            return (false, "Driving license Id does not exist.");

        var drivingLicense = _unitOfWork.DrivingLicenses.Find(dl => dl.Id == id).First();
        _unitOfWork.DrivingLicenses.Remove(drivingLicense);
        _unitOfWork.Complete();
        return (true, "Driving license deleted successfully.");
    }

    public (bool success, object result) DeleteOutdatedDrivingLicense()
    {
        var outdatedDrivingLicenseList = _unitOfWork.DrivingLicenses.Find(dl => dl.IssueDate <= DateTime.Now.AddYears(-10));

        _unitOfWork.DrivingLicenses.RemoveRange(outdatedDrivingLicenseList);
        _unitOfWork.Complete();
        return (true, "Outdated Driving License removed successfully.");
    }
}