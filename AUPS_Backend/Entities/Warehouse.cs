using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Entities;

[Table("warehouse")]
public partial class Warehouse
{
    [Key]
    [Column("warehouse_id")]
    public Guid WarehouseId { get; set; }

    [Column("address")]
    [StringLength(100)]
    public string Address { get; set; } = null!;

    [Column("city")]
    [StringLength(50)]
    public string City { get; set; } = null!;

    [Column("capacity")]
    public int Capacity { get; set; }

    [InverseProperty("Warehouse")]
    public virtual ICollection<ObjectOfLabor> ObjectOfLabors { get; set; } = new List<ObjectOfLabor>();
}
