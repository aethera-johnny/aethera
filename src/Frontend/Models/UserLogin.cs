using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "계정은 필수 항목입니다.")]
        [StringLength(50)]
        public required string UserAccount { get; set; }

        [Required(ErrorMessage = "비밀번호는 필수 항목입니다.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "비밀번호는 8자 이상이어야 합니다.")]
        public required string UserPassword { get; set; }
    }
}
