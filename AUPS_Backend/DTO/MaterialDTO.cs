namespace AUPS_Backend.DTO
{
    public class MaterialDTO
    {
        public Guid MaterialId { get; set; }

        public string? MaterialName { get; set; }

        public int StockQuantity { get; set; }

        public int TotalCount { get; set; }
    }
}
