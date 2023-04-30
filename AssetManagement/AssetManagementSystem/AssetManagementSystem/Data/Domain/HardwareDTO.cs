using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Data.Domain
{
    public class HardwareDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Not a valid name")]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Not a valid Type")]
        public string Type { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Incorrect Date Format")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(TypeName = "date")]
        public DateTime? DateOfPurchase { get; set; }

        [Required]
        [RegularExpression(@"^\d+(\.\d)?$", ErrorMessage = "Invalid Version")]
        public double Version { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z0-9]+)|([A-Za-z0-9]+))$", ErrorMessage = "Not a valid Model Number")]
        public string ModelNumber { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Quantity")]
        public int? Quantity { get; set; }

        [Required]
        [Range(100, int.MaxValue, ErrorMessage = "Please enter valid integer Cost")]
        public int? Cost { get; set; }
    }
}
