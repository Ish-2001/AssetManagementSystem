using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagementSystemUI.Models
{
    public class AssetAssignmentDTO
    {
        [Required]
		[RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$", ErrorMessage = "Not a valid Asset Id")]
		public string AssetId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Id")]
        public int? UserId { get; set; }

        [Required]
		[RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$", ErrorMessage = "Not a valid Asset Type")]
		public string AssetType { get; set; }

        [Required]
        [StringLength(10)]
		[RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$", ErrorMessage = "Not a valid Status")]
		public string Status { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Incorrect Date Format")]
        public DateTime AssignDate { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Incorrect Date Format")]
        public DateTime? UnassignDate { get; set; }

/*        [InverseProperty("UserDTO")]
        public virtual ICollection<AssetAssignmentDTO> AssetAssignments { get; } = new List<AssetAssignmentDTO>();*/
    }
}
