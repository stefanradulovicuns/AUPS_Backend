using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AUPS_Backend.DTO
{
    public class ObjectOfLaborDTO
    {
        public Guid ObjectOfLaborId { get; set; }

        public string? ObjectOfLaborName { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public Guid WarehouseId { get; set; }

        public string? WarehouseFullAddress { get; set; }

        public int TotalCount { get; set; }
    }
}
