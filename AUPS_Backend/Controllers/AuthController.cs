using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AUPS_Backend.Enums;
using AUPS_Backend.Identity;
using AUPS_Backend.Repositories;
using AUPS_Backend.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AUPS_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWorkplaceRepository _workplaceRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AuthController(IEmployeeRepository employeeRepository, IWorkplaceRepository workplaceRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IJwtService jwtService, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _workplaceRepository = workplaceRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        /*[HttpPost("register")]
        public async Task<ActionResult<AuthenticationResponse>> Register(RegisterDTO registerDTO)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.PersonName,
                PersonName = registerDTO.PersonName
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                if (registerDTO.UserType == UserTypeOptions.Admin)
                {
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole()
                        {
                            Name = UserTypeOptions.Admin.ToString()
                        };
                        await _roleManager.CreateAsync(applicationRole);
                    }
                }
                else
                {
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.User.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole()
                        {
                            Name = UserTypeOptions.User.ToString()
                        };
                        await _roleManager.CreateAsync(applicationRole);
                    }
                }

                await _userManager.AddToRoleAsync(user, registerDTO.UserType.ToString());
                await _signInManager.SignInAsync(user, isPersistent: false);

                var authenticationResponse = await _jwtService.CreateJwtToken(user);

                return Ok(authenticationResponse);
            }

            string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
            return Problem(errorMessage);
        }*/

        [HttpPost("registerFirstUser")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationResponse>> RegisterFirstUser(EmployeeCreateDTO employee)
        {
            var employees = await _employeeRepository.GetAllEmployees();
            if (employees.Any())
            {
                return BadRequest();
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                UserName = employee.Email,
                PersonName = employee.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, employee.Password);

            if (result.Succeeded)
            {
                if (await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
                {
                    ApplicationRole applicationRole = new ApplicationRole()
                    {
                        Name = UserTypeOptions.Admin.ToString()
                    };
                    await _roleManager.CreateAsync(applicationRole);
                }

                if (await _workplaceRepository.GetWorkplaceByName(UserTypeOptions.Admin.ToString()) is null)
                {
                    Workplace workplace = new Workplace()
                    {
                        WorkplaceName = UserTypeOptions.Admin.ToString()
                    };
                    var createdWorkplace = await _workplaceRepository.AddWorkplace(workplace);
                }

                await _userManager.AddToRoleAsync(user, UserTypeOptions.Admin.ToString());
                await _signInManager.SignInAsync(user, isPersistent: false);
                var adminWorkplace = await _workplaceRepository.GetWorkplaceByName(UserTypeOptions.Admin.ToString());
                if (adminWorkplace != null)
                {
                    employee.WorkplaceId = adminWorkplace.WorkplaceId;
                }
                var createdEmployee = await _employeeRepository.AddEmployee(_mapper.Map<Employee>(employee));

                var authenticationResponse = await _jwtService.CreateJwtToken(user);

                return Ok(authenticationResponse);
            }

            string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
            return Problem(errorMessage);
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationResponse>> Login(LoginDTO loginDTO)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null)
            {
                return Problem("Invalid email address");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var authenticationResponse = await _jwtService.CreateJwtToken(user);
                return Ok(authenticationResponse);
            }

            return Problem("Invalid email or password");
        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }
    }
}
