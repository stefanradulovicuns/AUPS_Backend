namespace AUPS_Backend.DTO
{
    public class ProductionOrderUpdateDTO
    {
        public Guid ProductionOrderId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Quantity { get; set; }

        public string? Note { get; set; }

        public Guid ObjectOfLaborId { get; set; }
    }
}
