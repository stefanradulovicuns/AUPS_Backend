using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AUPS_Backend.Enums;
using AUPS_Backend.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AUPS_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<EmployeeDTO>> GetEmployees(string? search, string? sortBy, SortOrderOptions? sortOrder, int page, int count)
        {
            var employees = await _employeeRepository.GetAllEmployees();

            if (!string.IsNullOrEmpty(search))
            {
                employees = employees.Where(e => e.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                || e.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                || e.Email.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            employees = (sortBy, sortOrder) switch
            {
                (nameof(EmployeeDTO.EmployeeId), SortOrderOptions.ASC) => employees.OrderBy(e => e.EmployeeId).ToList(),
                (nameof(EmployeeDTO.EmployeeId), SortOrderOptions.DESC) => employees.OrderByDescending(e => e.EmployeeId).ToList(),
                (nameof(EmployeeDTO.FirstName), SortOrderOptions.ASC) => employees.OrderBy(e => e.FirstName).ToList(),
                (nameof(EmployeeDTO.FirstName), SortOrderOptions.DESC) => employees.OrderByDescending(e => e.FirstName).ToList(),
                (nameof(EmployeeDTO.LastName), SortOrderOptions.ASC) => employees.OrderBy(e => e.LastName).ToList(),
                (nameof(EmployeeDTO.LastName), SortOrderOptions.DESC) => employees.OrderByDescending(e => e.LastName).ToList(),
                (nameof(EmployeeDTO.Email), SortOrderOptions.ASC) => employees.OrderBy(e => e.Email).ToList(),
                (nameof(EmployeeDTO.Email), SortOrderOptions.DESC) => employees.OrderByDescending(e => e.Email).ToList(),
                (nameof(EmployeeDTO.Password), SortOrderOptions.ASC) => employees.OrderBy(e => e.Password).ToList(),
                (nameof(EmployeeDTO.Password), SortOrderOptions.DESC) => employees.OrderByDescending(e => e.Password).ToList(),
                (nameof(EmployeeDTO.Jmbg), SortOrderOptions.ASC) => employees.OrderBy(e => e.Jmbg).ToList(),
                (nameof(EmployeeDTO.Jmbg), SortOrderOptions.DESC) => employees.OrderByDescending(e => e.Jmbg).ToList(),
                (nameof(EmployeeDTO.PhoneNumber), SortOrderOptions.ASC) => employees.OrderBy(e => e.PhoneNumber).ToList(),
                (nameof(EmployeeDTO.PhoneNumber), SortOrderOptions.DESC) => employees.OrderByDescending(e => e.PhoneNumber).ToList(),
                (nameof(EmployeeDTO.Address), SortOrderOptions.ASC) => employees.OrderBy(e => e.Address).ToList(),
                (nameof(EmployeeDTO.Address), SortOrderOptions.DESC) => employees.OrderByDescending(e => e.Address).ToList(),
                (nameof(EmployeeDTO.City), SortOrderOptions.ASC) => employees.OrderBy(e => e.City).ToList(),
                (nameof(EmployeeDTO.City), SortOrderOptions.DESC) => employees.OrderByDescending(e => e.City).ToList(),
                (nameof(EmployeeDTO.Sallary), SortOrderOptions.ASC) => employees.OrderBy(e => e.Sallary).ToList(),
                (nameof(EmployeeDTO.Sallary), SortOrderOptions.DESC) => employees.OrderByDescending(e => e.Sallary).ToList(),
                (nameof(EmployeeDTO.DateOfEmployment), SortOrderOptions.ASC) => employees.OrderBy(e => e.DateOfEmployment).ToList(),
                (nameof(EmployeeDTO.DateOfEmployment), SortOrderOptions.DESC) => employees.OrderByDescending(e => e.DateOfEmployment).ToList(),
                (nameof(EmployeeDTO.WorkplaceId), SortOrderOptions.ASC) => employees.OrderBy(e => e.WorkplaceId).ToList(),
                (nameof(EmployeeDTO.WorkplaceId), SortOrderOptions.DESC) => employees.OrderByDescending(e => e.WorkplaceId).ToList(),
                (nameof(EmployeeDTO.OrganizationalUnitId), SortOrderOptions.ASC) => employees.OrderBy(e => e.OrganizationalUnitId).ToList(),
                (nameof(EmployeeDTO.OrganizationalUnitId), SortOrderOptions.DESC) => employees.OrderByDescending(e => e.OrganizationalUnitId).ToList(),
                _ => employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToList(),
            };

            int totalCount = employees.Count();
            employees = employees.Skip(page > 0 ? (page - 1) * count : 0)
                .Take(count > 0 ? count : totalCount)
                .ToList();

            if (!employees.Any())
            {
                return NoContent();
            }

            var employeesDto = _mapper.Map<List<EmployeeDTO>>(employees);
            employeesDto[0].TotalCount = totalCount;

            return Ok(employeesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(Guid id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EmployeeDTO>(employee));
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> CreateEmployee(EmployeeCreateDTO employee)
        {
            var createdEmployee = await _employeeRepository.AddEmployee(_mapper.Map<Employee>(employee));

            return CreatedAtAction("GetEmployee", new { id = createdEmployee.EmployeeId }, _mapper.Map<EmployeeDTO>(createdEmployee));
        }

        [HttpPut]
        public async Task<ActionResult<EmployeeDTO>> UpdateEmployee(EmployeeUpdateDTO employee)
        {
            var matchingEmployee = await _employeeRepository.GetEmployeeById(employee.EmployeeId);
            if (matchingEmployee == null)
            {
                return NotFound();
            }

            var updatedEmployee = await _employeeRepository.UpdateEmployee(_mapper.Map<Employee>(employee));

            return Ok(_mapper.Map<EmployeeDTO>(updatedEmployee));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            bool isDeleted = await _employeeRepository.DeleteEmployee(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
