namespace Services.Employee;

public interface IEmployeeInformationService
{
    public (bool success, object result) AddEmployee(string firstName, string lastName, int gender, DateTime dateOfBirth, int role);

    public (bool success, object result) DeleteEmployee(int id);

    public (bool success, object result) UpdateRoleEmployee(int employeeId, string firstname, string lastname, int gender,
        DateTime dateOfBirth, int role);

    public List<Models.Employee> GetAllEmployee();

    public (bool success, object result) GetEmployeeById(int id);

    public List<Models.Employee> GetEmployeeByRole(int role);

    public List<Models.Employee> GetFreeEmployees();

    public List<Models.Employee> SortByTasksDescendingly();

    public List<Models.Employee> SortByTasksAscendingly();
}