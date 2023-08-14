using AUPS_Backend.Entities;

namespace AUPS_Backend.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployees();

        Task<Employee?> GetEmployeeById(Guid id);

        Task<Employee> AddEmployee(Employee employee);

        Task<Employee> UpdateEmployee(Employee employee);

        Task<bool> DeleteEmployee(Guid id);
    }
}
