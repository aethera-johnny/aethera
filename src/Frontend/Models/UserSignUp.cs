using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class UserSignUp
    {
        [Required(ErrorMessage = "계정은 필수 항목입니다.")]
        [StringLength(50)]
        public required string UserAccount { get; set; }

        [Required(ErrorMessage = "비밀번호는 필수 항목입니다.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "비밀번호는 8자 이상이어야 합니다.")]
        public required string UserPassword { get; set; }

        [Required(ErrorMessage = "이름은 필수 항목입니다.")]
        [StringLength(15)]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "이메일은 필수 항목입니다.")]
        [EmailAddress(ErrorMessage = "유효하지 않은 이메일 주소입니다.")]
        [StringLength(255)]
        public required string UserEmail { get; set; }

        [Required(ErrorMessage = "휴대폰 번호는 필수 항목입니다.")]
        [RegularExpression(@"^010\d{8}$", ErrorMessage = "휴대폰 번호는 010으로 시작하는 11자리 숫자여야 합니다.")]
        public required string UserPhone { get; set; }
    }
}