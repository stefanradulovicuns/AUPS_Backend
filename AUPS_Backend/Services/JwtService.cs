using AUPS_Backend.DTO;
using AUPS_Backend.Identity;
using AUPS_Backend.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AUPS_Backend.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmployeeRepository _employeeRepository;

        public JwtService(IConfiguration configuration, UserManager<ApplicationUser> userManager, IEmployeeRepository employeeRepository)
        {
            _configuration = configuration;
            _userManager = userManager;
            _employeeRepository = employeeRepository;
        }

        public async Task<AuthenticationResponse> CreateJwtToken(ApplicationUser user)
        {
            DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

            List<Claim> claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                claimList.Add(new Claim(ClaimTypes.Role, role));
            }

            var employee = await _employeeRepository.GetEmployeeByEmail(user.Email);
            if (employee != null)
            {
                claimList.Add(new Claim(ClaimTypes.UserData, employee.OrganizationalUnit.OrganizationalUnitId.ToString()));
            }

            Claim[] claims = claimList.ToArray();

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: signingCredentials);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(tokenGenerator);

            return new AuthenticationResponse()
            {
                Email = user.Email,
                PersonName = user.PersonName,
                Token = token,
                ExpirationTime = expiration
            };
        }
    }
}
