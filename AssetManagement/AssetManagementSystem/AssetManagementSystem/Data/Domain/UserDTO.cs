using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Data.Domain
{
    public class UserDTO
    {
        [Required]
        [StringLength(20)]
        //[RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$")]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$", ErrorMessage = "Not a valid first name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$", ErrorMessage = "Not a valid last name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Incorrect Date Format")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(30)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        //[RegularExpression("^(?=.*[A-Za-z])(?=.*[0-9])(?=(?:.*?[!@#$%\\^&*\\(\\)\\-_+=;:'\"\"\\/\\[\\]{},.<>|`]){2}).{8,32}$")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
