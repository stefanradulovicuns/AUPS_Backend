namespace AUPS_Backend.DTO
{
    public class ObjectOfLaborTechnologicalProcedureCreateDTO
    {
        public Guid ObjectOfLaborId { get; set; }

        public int OrderOfExecution { get; set; }

        public Guid TechnologicalProcedureId { get; set; }
    }
}
