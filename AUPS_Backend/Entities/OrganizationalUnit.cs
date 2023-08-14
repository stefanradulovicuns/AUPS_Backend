using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Entities;

[Table("organizational_unit")]
[Index("OrganizationalUnitName", Name = "UQ__organiza__723090DBB49B3EF8", IsUnique = true)]
public partial class OrganizationalUnit
{
    [Key]
    [Column("organizational_unit_id")]
    public Guid OrganizationalUnitId { get; set; }

    [Column("organizational_unit_name")]
    [StringLength(100)]
    public string OrganizationalUnitName { get; set; } = null!;

    [InverseProperty("OrganizationalUnit")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("OrganizationalUnitNavigation")]
    public virtual ICollection<TechnologicalProcedure> TechnologicalProcedures { get; set; } = new List<TechnologicalProcedure>();
}
