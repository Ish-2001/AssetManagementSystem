using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Data.Models;

public partial class Book
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string SerialNumber { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string Name { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string Author { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string Genre { get; set; }

    public int? Price { get; set; }

    public int? Quantity { get; set; }

    [Column(TypeName = "date")]
    public DateTime? DateOfPublish { get; set; }
}
