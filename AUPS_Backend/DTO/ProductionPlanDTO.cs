namespace AUPS_Backend.DTO
{
    public class ProductionPlanDTO
    {
        public Guid ProductionPlanId { get; set; }

        public string? ProductionPlanName { get; set; }

        public string? Description { get; set; }

        public Guid ObjectOfLaborId { get; set; }

        public string? ObjectOfLaborName { get; set; }

        public int TotalCount { get; set; }
    }
}
