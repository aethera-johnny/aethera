using System.ComponentModel.DataAnnotations;

namespace AuthService.Models
{
    public class UserLogin
    {
        [Required]
        public string UserAccount { get; set; }

        [Required]
        public string UserPassword { get; set; }
    }
}
