
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AssetManagementSystemUI.Models
{
    public class BookDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Not a valid name")]
      
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Not a valid author name")]
        [DisplayName("Author")]
        public string Source { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Quantity")]
        public int? Quantity { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Not a valid Genre")]
        [DisplayName("Genre")]
        public string Type { get; set; }

        [Required]
        [Range(50, int.MaxValue, ErrorMessage = "Please enter valid integer Price")]
        public int? Price { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Incorrect Date Format")]
        [DisplayName("Date Of Publish")]
        public DateTime Date { get; set; }

        [DisplayName("Category Id")]
		[Range(100, int.MaxValue, ErrorMessage = "Please enter valid integer Category Id")]
		public int CategoryId { get; set; }

        [StringLength(4)]
        [DisplayName("Serial Number")]
		
		public string IdentityNumber { get; set; }

    }
}
