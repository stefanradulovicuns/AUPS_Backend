namespace AUPS_Backend.DTO
{
    public class ProductionPlanCreateDTO
    { 
        public string? ProductionPlanName { get; set; }

        public string? Description { get; set; }

        public Guid ObjectOfLaborId { get; set; }
    }
}
