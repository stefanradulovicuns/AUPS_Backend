using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Entities;

[Table("production_plan")]
public partial class ProductionPlan
{
    [Key]
    [Column("production_plan_id")]
    public Guid ProductionPlanId { get; set; }

    [Column("production_plan_name")]
    [StringLength(100)]
    public string ProductionPlanName { get; set; } = null!;

    [Column("description")]
    public string Description { get; set; } = null!;

    [Column("object_of_labor_id")]
    public Guid ObjectOfLaborId { get; set; }

    [ForeignKey("ObjectOfLaborId")]
    [InverseProperty("ProductionPlans")]
    public virtual ObjectOfLabor ObjectOfLabor { get; set; } = null!;
}
