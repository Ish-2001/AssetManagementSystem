using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystemUI.Models
{
    public class AdminLoginDTO
    {
        [Required]
        [StringLength(30)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^((?=.*@)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Not a valid Password")]
        public string Password { get; set; }
    }
}
