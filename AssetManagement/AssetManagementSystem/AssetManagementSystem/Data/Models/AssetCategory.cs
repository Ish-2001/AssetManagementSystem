using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Data.Models;

[Table("AssetCategory")]
public partial class AssetCategory
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string SerialId { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<AssetDetail> AssetDetails { get; } = new List<AssetDetail>();
}
