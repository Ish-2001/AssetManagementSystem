using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace AssetManagementSystemUI.Models
{
    public class AssetDTO
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$", ErrorMessage = "Not a valid Type")]
        [DisplayName("Asset")]
        public string AssetName { get; set; }

        public int UserId { get; set; }

        [DisplayName("User Name")]
		[RegularExpression(@"^[A-za-z]{1}((-|\s)*[A-Za-z])*$", ErrorMessage = "Not a valid Type")]
		public string UserName { get; set; }

        [DisplayName("User Name")]
        [RegularExpression(@"^[A-za-z]{1}((-|\s)*[A-Za-z])*$", ErrorMessage = "Not a valid Type")]
        public string FirstName { get; set; }

        [DisplayName("Asset Id")]
		public string AssetId { get; set; }

        public string Status { get; set; }

        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Not a valid Asset Type")]
		[DisplayName("Asset Type")]
		public string AssetType { get; set; }

        [DisplayName("Assign Date")]
        [DataType(DataType.Date, ErrorMessage = "Incorrect Date Format")]
        public DateTime? AssignDate { get; set; }

        public int Quantity { get; set; }

        [DisplayName("Unassign Date")]
        [DataType(DataType.Date, ErrorMessage = "Incorrect Date Format")]
        public DateTime? UnassignDate { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("AssetAssignments")]
        public virtual UserDTO User { get; set; }
    }
}
