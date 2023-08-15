namespace AUPS_Backend.DTO
{
    public class WarehouseUpdateDTO
    {
        public Guid WarehouseId { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public int Capacity { get; set; }
    }
}
