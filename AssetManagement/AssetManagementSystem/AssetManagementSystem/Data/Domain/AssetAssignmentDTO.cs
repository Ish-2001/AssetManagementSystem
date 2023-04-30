using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagementSystem.Data.Domain
{
    public class AssetAssignmentDTO
    {
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z0-9]+)|([A-Za-z0-9]+))$", ErrorMessage = "Not a valid Asset Id")]
        public string AssetId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Quantity")]
        public int? UserId { get; set; }

        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Not a valid Asset Type")]
        public string AssetType { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z0-9]+)|([A-Za-z0-9]+))$", ErrorMessage = "Not a valid Status")]
        public string Status { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Incorrect Date Format")]
        public DateTime AssignDate { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Incorrect Date Format")]
        public DateTime? UnassignDate { get; set; }
    }
}
