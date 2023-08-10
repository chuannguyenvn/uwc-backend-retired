using Models;
using Models.Types;
using Repositories;
using Utilities;

namespace Services.Profile;

public class UserProfileService : IUserProfileService
{
    private readonly UnitOfWork _unitOfWork;

    public UserProfileService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool success, string message)> AddSupervisorProfile(string firstName, string lastName, Gender gender,
        DateTime dateOfBirth)
    {
        var supervisorProfile = new SupervisorProfile
        {
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            DateOfBirth = dateOfBirth,
            Role = UserRole.Supervisor,
        };

        _unitOfWork.SupervisorProfiles.Add(supervisorProfile);
        await _unitOfWork.CompleteAsync();

        return (true, Prompts.SUCCESS);
    }

    public async Task<(bool success, string message)> AddCleanerProfile(string firstName, string lastName, Gender gender,
        DateTime dateOfBirth)
    {
        var cleanerProfile = new CleanerProfile
        {
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            DateOfBirth = dateOfBirth,
            Role = UserRole.Cleaner,
        };

        _unitOfWork.CleanerProfiles.Add(cleanerProfile);
        await _unitOfWork.CompleteAsync();

        return (true, Prompts.SUCCESS);
    }

    public async Task<(bool success, string message)> AddDriverProfile(string firstName, string lastName, Gender gender,
        DateTime dateOfBirth)
    {
        var driverProfile = new DriverProfile
        {
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            DateOfBirth = dateOfBirth,
            Role = UserRole.Driver,
        };

        _unitOfWork.DriverProfiles.Add(driverProfile);
        await _unitOfWork.CompleteAsync();

        return (true, Prompts.SUCCESS);
    }


    public async Task<(bool success, string message)> DeleteUserProfile(int employeeId)
    {
        var employee = _unitOfWork.DriverProfiles.GetById(employeeId);
        if (employee == null)
            return (false, Prompts.EMPLOYEE_NOT_EXIST);

        _unitOfWork.DriverProfiles.Remove(employee);
        await _unitOfWork.CompleteAsync();

        return (true, Prompts.SUCCESS);
    }

    public async Task<(bool success, string message)> UpdateUserProfile(int employeeId, string firstName, string lastName, Gender gender,
        DateTime dateOfBirth, UserRole role)
    {
        var employee = _unitOfWork.DriverProfiles.GetById(employeeId);
        if (employee == null) return (false, Prompts.EMPLOYEE_NOT_EXIST);

        employee.FirstName = firstName;
        employee.LastName = lastName;
        employee.Gender = gender;
        employee.DateOfBirth = dateOfBirth;
        employee.Role = role;

        await _unitOfWork.CompleteAsync();

        return (true, Prompts.SUCCESS);
    }

    public async Task<(bool success, string message, List<UserProfile> result)> GetAllUserProfiles()
    {
        IEnumerable<UserProfile> supervisorProfiles = _unitOfWork.SupervisorProfiles.GetAll();
        IEnumerable<UserProfile> cleanerProfiles = _unitOfWork.CleanerProfiles.GetAll();
        IEnumerable<UserProfile> driverProfiles = _unitOfWork.DriverProfiles.GetAll();

        return (true, Prompts.SUCCESS, supervisorProfiles.Concat(cleanerProfiles.Concat(driverProfiles)).ToList());
    }

    public async Task<(bool success, string message, UserProfile result)> GetUserProfileById(int id)
    {
        if (!_unitOfWork.DriverProfiles.DoesIdExist(id)) return (false, Prompts.EMPLOYEE_NOT_EXIST, null);
        var employee = _unitOfWork.DriverProfiles.GetById(id);

        return (true, Prompts.SUCCESS, employee);
    }

    public async Task<(bool success, string message, List<UserProfile> result)> GetAllEmployeesWithRole(UserRole role)
    {
        switch (role)
        {
            case UserRole.Supervisor:
                IEnumerable<UserProfile> allSupervisorProfiles = _unitOfWork.SupervisorProfiles.GetAll();
                return (true, Prompts.SUCCESS, allSupervisorProfiles.ToList());
            case UserRole.Cleaner:
                IEnumerable<UserProfile> allCleanerProfiles = _unitOfWork.CleanerProfiles.GetAll();
                return (true, Prompts.SUCCESS, allCleanerProfiles.ToList());
            case UserRole.Driver:
                IEnumerable<UserProfile> allDriverProfiles = _unitOfWork.DriverProfiles.GetAll();
                return (true, Prompts.SUCCESS, allDriverProfiles.ToList());
            default:
                throw new ArgumentOutOfRangeException(nameof(role), role, null);
        }
    }
}