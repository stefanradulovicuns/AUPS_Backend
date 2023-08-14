using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Entities;

[Table("technological_procedure")]
[Index("TechnologicalProcedureName", Name = "UQ__technolo__07C953C84EF6CF04", IsUnique = true)]
public partial class TechnologicalProcedure
{
    [Key]
    [Column("technological_procedure_id")]
    public Guid TechnologicalProcedureId { get; set; }

    [Column("technological_procedure_name")]
    [StringLength(100)]
    public string TechnologicalProcedureName { get; set; } = null!;

    [Column("duration")]
    public int Duration { get; set; }

    [Column("organizational_unit")]
    public Guid OrganizationalUnit { get; set; }

    [Column("plant_id")]
    public Guid PlantId { get; set; }

    [Column("technological_system")]
    public Guid TechnologicalSystem { get; set; }

    [InverseProperty("TechnologicalProcedure")]
    public virtual ICollection<ObjectOfLaborTechnologicalProcedure> ObjectOfLaborTechnologicalProcedures { get; set; } = new List<ObjectOfLaborTechnologicalProcedure>();

    [ForeignKey("OrganizationalUnit")]
    [InverseProperty("TechnologicalProcedures")]
    public virtual OrganizationalUnit OrganizationalUnitNavigation { get; set; } = null!;

    [ForeignKey("PlantId")]
    [InverseProperty("TechnologicalProcedures")]
    public virtual Plant Plant { get; set; } = null!;

    [ForeignKey("TechnologicalSystem")]
    [InverseProperty("TechnologicalProcedures")]
    public virtual TechnologicalSystem TechnologicalSystemNavigation { get; set; } = null!;
}
