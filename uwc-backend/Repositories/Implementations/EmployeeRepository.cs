using Models;

namespace Repositories.Implementations;

public class EmployeeRepository : GenericRepository<Employee>
{
    public EmployeeRepository(UwcDbContext context) : base(context)
    {
    }

    public IEnumerable<Employee> GetEmployeesWithRole(int role)
    {
        return _context.Employees.Where(employee => employee.Role == role);
    }
}