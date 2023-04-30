using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Data.Models;

[Table("AssetAssignment")]
public partial class AssetAssignment
{
    [Key]
    public int Id { get; set; }

    public int? UserId { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string AssetId { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string AssetType { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string Status { get; set; }

    [Column(TypeName = "date")]
    public DateTime AssignDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime? UnassignDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("AssetAssignments")]
    public virtual User User { get; set; }
}
