namespace AUPS_Backend.DTO
{
    public class AuthenticationResponse
    {
        public string? PersonName { get; set; }

        public string? Email { get; set; }

        public string? Token { get; set; }

        public DateTime ExpirationTime { get; set; }
    }
}
