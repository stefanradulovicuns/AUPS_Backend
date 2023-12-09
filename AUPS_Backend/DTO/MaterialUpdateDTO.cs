namespace AUPS_Backend.DTO
{
    public class MaterialUpdateDTO
    {
        public Guid MaterialId { get; set; }

        public string? MaterialName { get; set; }

        public int StockQuantity { get; set; }
    }
}
