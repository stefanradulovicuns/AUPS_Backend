using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AUPS_Backend.DTO
{
    public class WarehouseDTO
    {
        public Guid WarehouseId { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public int Capacity { get; set; }

        public int TotalCount { get; set; }
    }
}
