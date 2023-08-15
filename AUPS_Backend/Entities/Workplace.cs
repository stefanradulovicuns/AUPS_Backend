using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Entities;

[Table("workplace")]
[Index("WorkplaceName", Name = "UQ__workplac__F9F5552C94E5B3AF", IsUnique = true)]
public partial class Workplace
{
    [Key]
    [Column("workplace_id")]
    public Guid WorkplaceId { get; set; }

    [Column("workplace_name")]
    [StringLength(100)]
    public string WorkplaceName { get; set; } = null!;

    [InverseProperty("Workplace")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
