namespace AUPS_Backend.DTO
{
    public class ProductionPlanUpdateDTO
    {
        public Guid ProductionPlanId { get; set; }

        public string? ProductionPlanName { get; set; }

        public string? Description { get; set; }

        public Guid ObjectOfLaborId { get; set; }
    }
}
