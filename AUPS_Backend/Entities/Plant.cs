using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Entities;

[Table("plant")]
[Index("PlantName", Name = "UQ__plant__2D6424534E0AE92D", IsUnique = true)]
public partial class Plant
{
    [Key]
    [Column("plant_id")]
    public Guid PlantId { get; set; }

    [Column("plant_name")]
    [StringLength(100)]
    public string PlantName { get; set; } = null!;

    [InverseProperty("Plant")]
    public virtual ICollection<TechnologicalProcedure> TechnologicalProcedures { get; set; } = new List<TechnologicalProcedure>();
}
