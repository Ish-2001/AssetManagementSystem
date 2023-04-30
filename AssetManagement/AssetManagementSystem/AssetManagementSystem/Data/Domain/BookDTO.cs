using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Data.Domain
{
    public class AssetDetailDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z0-9]+)|([A-Za-z0-9]+))$", ErrorMessage = "Not a valid Serial Number")]
        public string SerialNumber { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Not a valid name")]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Not a valid author name")]
        public string Author { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Quantity")]
        public int? Quantity { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Not a valid Genre")]
        public string Genre { get; set; }

        [Required]
        [Range(50, int.MaxValue, ErrorMessage = "Please enter valid integer Price")]
        public int? Price { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Incorrect Date Format")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfPublish { get; set; }
    }
}
