using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Entities;

[Table("production_order")]
public partial class ProductionOrder
{
    [Key]
    [Column("production_order_id")]
    public Guid ProductionOrderId { get; set; }

    [Column("start_date", TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Column("end_date", TypeName = "date")]
    public DateTime EndDate { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("note")]
    public string Note { get; set; } = null!;

    [Column("current_technological_procedure")]
    public int CurrentTechnologicalProcedure { get; set; }

    [Column("current_technological_procedure_executed")]
    public bool CurrentTechnologicalProcedureExecuted { get; set; }

    [Column("employee_id")]
    public Guid EmployeeId { get; set; }

    [Column("object_of_labor_id")]
    public Guid ObjectOfLaborId { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("ProductionOrders")]
    public virtual Employee Employee { get; set; } = null!;

    [ForeignKey("ObjectOfLaborId")]
    [InverseProperty("ProductionOrders")]
    public virtual ObjectOfLabor ObjectOfLabor { get; set; } = null!;
}
