using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystemUI.Models
{
    public class UserLoginDTO
    {
        [Required]
        [StringLength(20)]
        [DisplayName("User Name")]
        [RegularExpression(@"[a-zA-Z][a-zA-Z0-9@]{1}", ErrorMessage = "Not a valid user name") ]
        public string UserName { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^((?=.*@)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Not a valid Password")]
        public string Password { get; set; }
    }
}
