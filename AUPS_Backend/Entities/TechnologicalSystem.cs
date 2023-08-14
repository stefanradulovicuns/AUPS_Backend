using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Entities;

[Table("technological_system")]
[Index("TechnologicalSystemName", Name = "UQ__technolo__F6E71529168A0AFC", IsUnique = true)]
public partial class TechnologicalSystem
{
    [Key]
    [Column("technological_system_id")]
    public Guid TechnologicalSystemId { get; set; }

    [Column("technological_system_name")]
    [StringLength(100)]
    public string TechnologicalSystemName { get; set; } = null!;

    [InverseProperty("TechnologicalSystemNavigation")]
    public virtual ICollection<TechnologicalProcedure> TechnologicalProcedures { get; set; } = new List<TechnologicalProcedure>();
}
