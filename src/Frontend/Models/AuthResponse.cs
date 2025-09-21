namespace Frontend.Models
{
    public class AuthResponse
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required DateTime RefreshTokenExpiryTime { get; set; }
        public required int UserId { get; set; }
        public required string UserAccount { get; set; }
        public required string UserName { get; set; }
        public required string UserEmail { get; set; }
    }
}
