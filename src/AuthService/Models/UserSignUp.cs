using System.ComponentModel.DataAnnotations;

namespace AuthService.Models
{
    public class UserSignUp
    {
        [Required(ErrorMessage = "User account is required.")]
        [StringLength(50)]
        public string UserAccount { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "User name is required.")]
        [StringLength(15)]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(255)]
        public string UserEmail { get; set; }

        [StringLength(11)]
        public string UserPhone { get; set; }
    }
}
