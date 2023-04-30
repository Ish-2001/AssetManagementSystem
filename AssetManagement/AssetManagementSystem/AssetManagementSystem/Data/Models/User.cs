using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Data.Models;

public partial class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string UserName { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string LastName { get; set; }

    [Column(TypeName = "date")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [StringLength(30)]
    [Unicode(false)]
    public string Email { get; set; }

    [Required]
    [StringLength(10)]
    public string PhoneNumber { get; set; }

    [Required]
    [Unicode(false)]
    public string Password { get; set; }

    public bool IsAdmin { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<AssetAssignment> AssetAssignments { get; } = new List<AssetAssignment>();
}
