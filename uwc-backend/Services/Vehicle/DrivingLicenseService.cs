using Models;
using Repositories;
using Types;

namespace Services.Vehicle;

public interface IDrivingLicenseService
{
    public (bool success, object result) AddDrivingLicense(DateTime issueDate, string issuePlace, int ownerId, string type);

    public List<DrivingLicense> GetDrivingLicenseOfDriver(int id);

    public (bool success, object result) UpdateDrivingLicense(int id, DateTime issueDate, string issuePlace, int ownerId, string type);

    public (bool success, object result) DeleteDrivingLicense(int id);

    public (bool success, object result) DeleteAllOutdatedDrivingLicenses();
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
        if (!_unitOfWork.DriverProfiles.DoesIdExist(ownerId))
            return (false, "Employee Id does not exist.");

        var owner = _unitOfWork.DriverProfiles.Find(employee => employee.Id == ownerId).First();

        if (owner.Role != UserRole.Driver) return (false, "Employee Id is not a driver");

        var drivingLicenseInformation =
            new DrivingLicense {IssueDate = issueDate, IssuePlace = issuePlace, DriverProfile = owner, Type = type};

        _unitOfWork.DrivingLicenses.Add(drivingLicenseInformation);
        _unitOfWork.Complete();

        return (true, "Driving license added successfully");
    }

    public List<DrivingLicense> GetDrivingLicenseOfDriver(int id)
    {
        if (!_unitOfWork.DriverProfiles.DoesIdExist(id)) return new List<DrivingLicense>();

        var drivingLicenseList = _unitOfWork.DrivingLicenses.Find(dl => dl.DriverProfile.Id == id);
        return drivingLicenseList.ToList();
    }

    public (bool success, object result) UpdateDrivingLicense(int id, DateTime issueDate, string issuePlace, int ownerId, string type)
    {
        if (!_unitOfWork.DrivingLicenses.DoesIdExist(id))
            return (false, "Driving license Id does not exist.");

        var drivingLicense = _unitOfWork.DrivingLicenses.Find(dl => dl.Id == id).First();
        var owner = _unitOfWork.DriverProfiles.Find(employee => employee.Id == ownerId).First();

        if (owner.Role != UserRole.Driver) return (false, "Owner is not a driver");

        drivingLicense.IssueDate = issueDate;
        drivingLicense.IssuePlace = issuePlace;
        drivingLicense.DriverProfile = owner;
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

    public (bool success, object result) DeleteAllOutdatedDrivingLicenses()
    {
        var outdatedDrivingLicenseList = _unitOfWork.DrivingLicenses.Find(dl => dl.IssueDate <= DateTime.Now.AddYears(-10));

        _unitOfWork.DrivingLicenses.RemoveRange(outdatedDrivingLicenseList);
        _unitOfWork.Complete();
        return (true, "Outdated Driving License removed successfully.");
    }
}