using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Entities;

[Table("object_of_labor")]
[Index("ObjectOfLaborName", Name = "UQ__object_o__55DFE9561D13D691", IsUnique = true)]
public partial class ObjectOfLabor
{
    [Key]
    [Column("object_of_labor_id")]
    public Guid ObjectOfLaborId { get; set; }

    [Column("object_of_labor_name")]
    [StringLength(100)]
    public string ObjectOfLaborName { get; set; } = null!;

    [Column("description")]
    public string Description { get; set; } = null!;

    [Column("price", TypeName = "numeric(10, 2)")]
    public decimal? Price { get; set; }

    [Column("stock_quantity")]
    public int StockQuantity { get; set; }

    [Column("warehouse_id")]
    public Guid WarehouseId { get; set; }

    [InverseProperty("ObjectOfLabor")]
    public virtual ICollection<ObjectOfLaborTechnologicalProcedure> ObjectOfLaborTechnologicalProcedures { get; set; } = new List<ObjectOfLaborTechnologicalProcedure>();

    [InverseProperty("ObjectOfLabor")]
    public virtual ICollection<ProductionOrder> ProductionOrders { get; set; } = new List<ProductionOrder>();

    [InverseProperty("ObjectOfLabor")]
    public virtual ICollection<ProductionPlan> ProductionPlans { get; set; } = new List<ProductionPlan>();

    [ForeignKey("WarehouseId")]
    [InverseProperty("ObjectOfLabors")]
    public virtual Warehouse Warehouse { get; set; } = null!;
}
