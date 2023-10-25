using AUPS_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AupsContext _context;

        public EmployeeRepository(AupsContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid();
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteEmployee(Guid id)
        {
            _context.Employees.RemoveRange(_context.Employees.Where(e => e.EmployeeId == id));
            int rowsDeleted = await _context.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<Employee?> GetEmployeeById(Guid id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            Employee? matchingEmployee = await GetEmployeeById(employee.EmployeeId);

            if (matchingEmployee == null)
            {
                return employee;
            }

            matchingEmployee.FirstName = employee.FirstName;
            matchingEmployee.LastName = employee.LastName;
            matchingEmployee.Email = employee.Email;
            matchingEmployee.Password = employee.Password;
            matchingEmployee.Jmbg = employee.Jmbg;
            matchingEmployee.PhoneNumber = employee.PhoneNumber;
            matchingEmployee.Address = employee.Address;
            matchingEmployee.City = employee.City;
            matchingEmployee.Sallary = employee.Sallary;
            matchingEmployee.DateOfEmployment = employee.DateOfEmployment;
            matchingEmployee.WorkplaceId = employee.WorkplaceId;
            matchingEmployee.OrganizationalUnitId = employee.OrganizationalUnitId;

            await _context.SaveChangesAsync();
            return matchingEmployee;
        }

        public async Task<Employee?> GetEmployeeByEmail(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
