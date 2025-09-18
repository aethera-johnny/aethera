using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.Entities
{
    [Table("tb_user_info")]
    public class Tb_User_Info
    {
        [Key]
        [Column("user_id", Order = 1)]
        public int User_Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("user_account", Order = 2)]
        public string UserAccount { get; set; }

        [Required]
        [StringLength(64)]
        [Column("user_password", Order = 3)]
        public string UserPassword { get; set; }

        [Required]
        [Column("password_salt", Order = 4)]
        public byte[] PasswordSalt { get; set; }

        [Required]
        [StringLength(15)]
        [Column("user_name", Order = 5)]
        public string UserName { get; set; }

        [StringLength(11)]
        [Column("user_phone", Order = 6)]
        public string UserPhone { get; set; }

        [StringLength(255)]
        [Column("user_email", Order = 7)]
        public string UserEmail { get; set; }

        [Column("refresh_token", Order = 8)]
        public string RefreshToken { get; set; }

        [Column("refresh_token_expiry_time", Order = 9)]
        public DateTime RefreshTokenExpiryTime { get; set; }

        [Column("created_datetime", Order = 10)]
        public DateTime CreatedDatetime { get; set; }

        [Column("updated_datetime", Order = 11)]
        public DateTime UpdatedDatetime { get; set; }
    }
}
