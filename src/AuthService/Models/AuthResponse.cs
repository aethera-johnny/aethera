using System;

namespace AuthService.Models
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public int UserId { get; set; }
        public string UserAccount { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
