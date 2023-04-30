using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystemUI.Models
{
    public class AssetCategoryDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$", ErrorMessage = "Not a valid Name")]
        public string Name { get; set; }

		[Required]
		[StringLength(10)]
		public string SerialId { get; set; }
	}
}
