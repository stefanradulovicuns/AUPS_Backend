using AUPS_Backend.Enums;

namespace AUPS_Backend.DTO
{
    public class RegisterDTO
    {
        public string? PersonName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        public UserTypeOptions UserType { get; set; } = UserTypeOptions.User;
    }
}
