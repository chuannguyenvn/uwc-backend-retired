using Models;
using Commons.Types;

namespace Services.Profile;

public interface IUserProfileService
{
    public Task<(bool success, string message)>
        AddSupervisorProfile(string firstName, string lastName, Gender gender, DateTime dateOfBirth);

    public Task<(bool success, string message)> AddCleanerProfile(string firstName, string lastName, Gender gender, DateTime dateOfBirth);

    public Task<(bool success, string message)> AddDriverProfile(string firstName, string lastName, Gender gender, DateTime dateOfBirth);

    public Task<(bool success, string message)> DeleteUserProfile(int employeeId);

    public Task<(bool success, string message)> UpdateUserProfile(int employeeId, string firstname, string lastname, Gender gender,
        DateTime dateOfBirth, UserRole userRole);

    public Task<(bool success, string message, List<UserProfile> result)> GetAllUserProfiles();

    public Task<(bool success, string message, UserProfile result)> GetUserProfileById(int id);

    public Task<(bool success, string message, List<UserProfile> result)> GetAllEmployeesWithRole(UserRole userRole);
}