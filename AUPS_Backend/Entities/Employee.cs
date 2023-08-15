using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Entities;

[Table("employee")]
[Index("Jmbg", Name = "UQ__employee__8C39FC6751DCDFA0", IsUnique = true)]
[Index("Email", Name = "UQ__employee__AB6E61644279CDEF", IsUnique = true)]
public partial class Employee
{
    [Key]
    [Column("employee_id")]
    public Guid EmployeeId { get; set; }

    [Column("first_name")]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column("password")]
    public string Password { get; set; } = null!;

    [Column("jmbg")]
    [StringLength(13)]
    public string Jmbg { get; set; } = null!;

    [Column("phone_number")]
    [StringLength(15)]
    public string PhoneNumber { get; set; } = null!;

    [Column("address")]
    [StringLength(100)]
    public string Address { get; set; } = null!;

    [Column("city")]
    [StringLength(50)]
    public string City { get; set; } = null!;

    [Column("sallary", TypeName = "numeric(10, 2)")]
    public decimal Sallary { get; set; }

    [Column("date_of_employment", TypeName = "date")]
    public DateTime DateOfEmployment { get; set; }

    [Column("workplace_id")]
    public Guid WorkplaceId { get; set; }

    [Column("organizational_unit_id")]
    public Guid OrganizationalUnitId { get; set; }

    [ForeignKey("OrganizationalUnitId")]
    [InverseProperty("Employees")]
    public virtual OrganizationalUnit OrganizationalUnit { get; set; } = null!;

    [InverseProperty("Employee")]
    public virtual ICollection<ProductionOrder> ProductionOrders { get; set; } = new List<ProductionOrder>();

    [ForeignKey("WorkplaceId")]
    [InverseProperty("Employees")]
    public virtual Workplace Workplace { get; set; } = null!;
}
