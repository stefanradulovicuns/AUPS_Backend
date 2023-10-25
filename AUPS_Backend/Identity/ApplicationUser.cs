using Microsoft.AspNetCore.Identity;

namespace AUPS_Backend.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? PersonName { get; set; }
    }
}
