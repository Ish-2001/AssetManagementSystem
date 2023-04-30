using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Data.Models;

public partial class AssetDetail
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Source { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Date { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Type { get; set; }

    public int? Quantity { get; set; }

    public int? Price { get; set; }

    public int CategoryId { get; set; }

    [Required]
    [StringLength(4)]
    [Unicode(false)]
    public string IdentityNumber { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("AssetDetails")]
    public virtual AssetCategory Category { get; set; }
}
