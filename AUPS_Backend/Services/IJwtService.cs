using AUPS_Backend.DTO;
using AUPS_Backend.Identity;

namespace AUPS_Backend.Services
{
    public interface IJwtService
    {
        Task<AuthenticationResponse> CreateJwtToken(ApplicationUser user);
    }
}
