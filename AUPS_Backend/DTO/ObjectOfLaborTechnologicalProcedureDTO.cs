namespace AUPS_Backend.DTO
{
    public class ObjectOfLaborTechnologicalProcedureDTO
    {
        public Guid ObjectOfLaborTechnologicalProcedureId { get; set; }

        public Guid ObjectOfLaborId { get; set; }

        public Guid TechnologicalProcedureId { get; set; }

        public int TotalCount { get; set; }
    }
}
