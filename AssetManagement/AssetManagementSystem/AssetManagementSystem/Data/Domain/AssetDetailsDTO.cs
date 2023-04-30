﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Data.Domain
{
    public class AssetDetailsDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$", ErrorMessage = "Not a valid Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$", ErrorMessage = "Not a valid Source")]
        public string Source { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-za-z]*((-|\s)*[A-Za-z])*$", ErrorMessage = "Not a valid Type")]
        public string Type { get; set; }

        public int? Quantity { get; set; }

        public int? Price { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [StringLength(4)]
        public string IdentityNumber { get; set; }
    }
}
