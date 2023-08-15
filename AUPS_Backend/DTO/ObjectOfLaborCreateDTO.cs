namespace AUPS_Backend.DTO
{
    public class ObjectOfLaborCreateDTO
    {
        public string? ObjectOfLaborName { get; set; } = null!;

        public string? Description { get; set; } = null!;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public Guid WarehouseId { get; set; }
    }
}
