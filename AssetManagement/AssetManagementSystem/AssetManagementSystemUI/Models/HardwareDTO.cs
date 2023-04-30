using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AssetManagementSystemUI.Models
{
    public class HardwareDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$", ErrorMessage = "Not a valid name")]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$", ErrorMessage = "Not a valid Type")]
        [DisplayName("Manufacturer")]
        public string Source { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$", ErrorMessage = "Not a valid Type")]
        public string Type { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Incorrect Date Format")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Date Of Purchase")]
        public DateTime? Date { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z0-9]+)|([A-Za-z0-9]+))$", ErrorMessage = "Not a valid Model Number")]
        [DisplayName("Model Number")]
        public string IdentityNumber { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Quantity")]
        public int? Quantity { get; set; }

        [Required]
        [Range(100, int.MaxValue, ErrorMessage = "Please enter valid integer Cost")]
        [DisplayName("Price")]
        public int? Price { get; set; }

        [DisplayName("Category Id")]
		[Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer category Id")]
		public int CategoryId { get; set; }
    }
}
