using Models;

namespace Repositories.Implementations;

public class EmployeeProfileRepository : GenericRepository<EmployeeProfile>
{
    public EmployeeProfileRepository(UwcDbContext context) : base(context)
    {
    }

    public IEnumerable<EmployeeProfile> GetEmployeesWithRole(int role)
    {
        return _context.Employees.Where(employee => employee.Role == role);
    }
}