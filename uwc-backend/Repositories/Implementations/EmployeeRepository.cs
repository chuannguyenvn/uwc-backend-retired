using Models;

namespace Repositories.Implementations;

public class EmployeeRepository : GenericRepository<Employee>
{
    public EmployeeRepository(UwcDbContext context) : base(context)
    {
    }

    public IEnumerable<Employee> GetEmployeesWithRole(string role)
    {
        return _context.Employees.Where(employee => employee.Role == role);
    }
}