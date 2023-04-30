using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Data.Domain
{
    public class AssetCategoryDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)] 
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$", ErrorMessage = "Not a valid Name")]
        public string Name { get; set; }
    }
}
