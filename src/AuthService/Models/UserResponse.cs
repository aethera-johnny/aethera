using System;

namespace AuthService.Models
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string UserAccount { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public DateTime CreatedDatetime { get; set; }
    }
}
