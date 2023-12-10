namespace AUPS_Backend.DTO
{
    public class ObjectOfLaborMaterialDTO
    {
        public Guid ObjectOfLaborMaterialId { get; set; }

        public int Quantity { get; set; }

        public Guid ObjectOfLaborId { get; set; }

        public Guid MaterialId { get; set; }

        public string? MaterialName { get; set; }

        public string? StockQuantity { get; set; }

        public int TotalCount { get; set; }
    }
}
