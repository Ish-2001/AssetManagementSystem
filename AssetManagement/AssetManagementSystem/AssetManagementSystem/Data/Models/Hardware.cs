using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Data.Models;

public partial class Hardware
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string Name { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string Type { get; set; }

    [Column(TypeName = "date")]
    public DateTime? DateOfPurchase { get; set; }

    public double Version { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string ModelNumber { get; set; }

    public int? Quantity { get; set; }

    public int? Cost { get; set; }
}
