using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Data.Models;

public partial class SoftwareLicense
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(30)]
    [Unicode(false)]
    public string Name { get; set; }

    [Required]
    [StringLength(30)]
    [Unicode(false)]
    public string Type { get; set; }

    public int? InitialCost { get; set; }

    [Column(TypeName = "date")]
    public DateTime? ExpiryDate { get; set; }

    public int? Quantity { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string LicenseNo { get; set; }
}
