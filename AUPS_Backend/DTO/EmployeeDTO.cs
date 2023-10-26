using AUPS_Backend.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AUPS_Backend.DTO
{
    public class EmployeeDTO
    {
        public Guid EmployeeId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Jmbg { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public decimal Sallary { get; set; }

        public DateTime DateOfEmployment { get; set; }

        public Guid WorkplaceId { get; set; }

        public string? WorkplaceName { get; set; }

        public Guid OrganizationalUnitId { get; set; }

        public string? OrganizationalUnitName { get; set; }

        public int TotalCount { get; set; }
    }
}
