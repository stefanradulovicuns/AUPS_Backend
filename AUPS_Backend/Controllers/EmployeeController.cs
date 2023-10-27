using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AUPS_Backend.Enums;
using AUPS_Backend.Identity;
using AUPS_Backend.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AUPS_Backend.Controllers
{
    //[Authorize(Roles = nameof(UserTypeOptions.Admin) + "," + nameof(UserTypeOptions.User))]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWorkplaceRepository _workplaceRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public EmployeeController(IEmployeeRepository employeeRepository, IWorkplaceRepository workplaceRepository, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _employeeRepository = employeeRepository;
            _workplaceRepository = workplaceRepository;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
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
                (nameof(EmployeeDTO.WorkplaceName), SortOrderOptions.ASC) => employees.OrderBy(e => e.Workplace.WorkplaceName).ToList(),
                (nameof(EmployeeDTO.WorkplaceName), SortOrderOptions.DESC) => employees.OrderByDescending(e => e.Workplace.WorkplaceName).ToList(),
                (nameof(EmployeeDTO.OrganizationalUnitId), SortOrderOptions.ASC) => employees.OrderBy(e => e.OrganizationalUnitId).ToList(),
                (nameof(EmployeeDTO.OrganizationalUnitId), SortOrderOptions.DESC) => employees.OrderByDescending(e => e.OrganizationalUnitId).ToList(),
                (nameof(EmployeeDTO.OrganizationalUnitName), SortOrderOptions.ASC) => employees.OrderBy(e => e.OrganizationalUnit.OrganizationalUnitName).ToList(),
                (nameof(EmployeeDTO.OrganizationalUnitName), SortOrderOptions.DESC) => employees.OrderByDescending(e => e.OrganizationalUnit.OrganizationalUnitName).ToList(),
                _ => employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToList(),
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

            ApplicationUser user = new ApplicationUser()
            {
                Email = createdEmployee.Email,
                PhoneNumber = createdEmployee.PhoneNumber,
                UserName = createdEmployee.Email,
                PersonName = createdEmployee.Email
            };

            var workplace = await _workplaceRepository.GetWorkplaceById(createdEmployee.WorkplaceId);

            IdentityResult result = await _userManager.CreateAsync(user, employee.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, workplace?.WorkplaceName);
            }

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

            Employee oldEmployee = new Employee()
            {
                EmployeeId = matchingEmployee.EmployeeId,
                FirstName = matchingEmployee.FirstName,
                LastName = matchingEmployee.LastName,
                Email = matchingEmployee.Email,
                Jmbg = matchingEmployee.Jmbg,
                PhoneNumber = matchingEmployee.PhoneNumber,
                Address = matchingEmployee.Address,
                City = matchingEmployee.City,
                Sallary = matchingEmployee.Sallary,
                DateOfEmployment = matchingEmployee.DateOfEmployment,
                WorkplaceId = matchingEmployee.WorkplaceId,
                OrganizationalUnitId = matchingEmployee.OrganizationalUnitId
            };

            var updatedEmployee = await _employeeRepository.UpdateEmployee(_mapper.Map<Employee>(employee));
            var user = await _userManager.FindByEmailAsync(oldEmployee.Email);
            user.Email = updatedEmployee.Email;
            user.PhoneNumber = updatedEmployee.PhoneNumber;
            user.UserName = updatedEmployee.Email;
            user.PersonName = updatedEmployee.Email;
            
            var result = await _userManager.UpdateAsync(user);
            if (!string.IsNullOrEmpty(employee.Password))
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, employee.Password);
            }

            if (result.Succeeded)
            {
                if (oldEmployee.WorkplaceId != updatedEmployee.WorkplaceId)
                {
                    var oldWorkplace = await _workplaceRepository.GetWorkplaceById(oldEmployee.WorkplaceId);
                    var newWorkplace = await _workplaceRepository.GetWorkplaceById(updatedEmployee.WorkplaceId);
                    await _userManager.RemoveFromRoleAsync(user, oldWorkplace?.WorkplaceName);
                    await _userManager.AddToRoleAsync(user, newWorkplace?.WorkplaceName);
                }
            }
            else
            {
                string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
                return Problem(errorMessage);
            }

            return Ok(_mapper.Map<EmployeeDTO>(updatedEmployee));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByEmailAsync(employee.Email);
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
                return Problem(errorMessage);
            }

            await _employeeRepository.DeleteEmployee(id);

            return NoContent();
        }
    }
}
