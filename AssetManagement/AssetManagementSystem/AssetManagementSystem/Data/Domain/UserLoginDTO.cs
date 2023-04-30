using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Data.Domain
{
    public class UserLoginDTO
    {
        [Required]
        [StringLength(20)]
        //[RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string UserName { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        //[RegularExpression("^(?=.*[A-Za-z])(?=.*[0-9])(?=(?:.*?[!@#$%\\^&*\\(\\)\\-_+=;:'\"\"\\/\\[\\]{},.<>|`]){2}).{8,32}$")]
        public string Password { get; set; }
    }
}
