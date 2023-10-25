using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Entities;

[Table("object_of_labor_technological_procedure")]
public partial class ObjectOfLaborTechnologicalProcedure
{
    [Key]
    [Column("object_of_labor_technological_procedure_id")]
    public Guid ObjectOfLaborTechnologicalProcedureId { get; set; }

    [Column("order_of_execution")]
    public int OrderOfExecution { get; set; }

    [Column("object_of_labor_id")]
    public Guid ObjectOfLaborId { get; set; }

    [Column("technological_procedure_id")]
    public Guid TechnologicalProcedureId { get; set; }

    [ForeignKey("ObjectOfLaborId")]
    [InverseProperty("ObjectOfLaborTechnologicalProcedures")]
    public virtual ObjectOfLabor ObjectOfLabor { get; set; } = null!;

    [ForeignKey("TechnologicalProcedureId")]
    [InverseProperty("ObjectOfLaborTechnologicalProcedures")]
    public virtual TechnologicalProcedure TechnologicalProcedure { get; set; } = null!;
}
