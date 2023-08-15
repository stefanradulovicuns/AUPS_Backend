namespace AUPS_Backend.DTO
{
    public class ObjectOfLaborUpdateDTO
    {
        public Guid ObjectOfLaborId { get; set; }

        public string? ObjectOfLaborName { get; set; } = null!;

        public string? Description { get; set; } = null!;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public Guid WarehouseId { get; set; }
    }
}
