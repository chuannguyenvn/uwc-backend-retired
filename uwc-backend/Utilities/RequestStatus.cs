namespace Utilities;

public static class RequestStatus
{
    public const string SUCCESS = "Success.";

    public static class Account
    {
        public const string ACCOUNT_NOT_EXIST = "Account does not exist.";
        public const string WRONG_PASSWORD = "Wrong password.";
        public const string PASSWORD_UPDATE_FAIL = "Password update failed.";
        public const string SETTINGS_UPDATE_FAIL = "Settings update failed.";
    }

    public static class Employee
    {
        public const string INVALID_ROLE = "Invalid role.";
        public const string INVALID_GENDER = "Invalid gender.";
        public const string EMPLOYEE_NOT_EXIST = "Employee does not exist.";
    }
}