namespace AUPS_Backend.DTO
{
    public class ObjectOfLaborTechnologicalProcedureUpdateDTO
    {
        public Guid ObjectOfLaborTechnologicalProcedureId { get; set; }

        public int OrderOfExecution { get; set; }

        public Guid ObjectOfLaborId { get; set; }

        public Guid TechnologicalProcedureId { get; set; }
    }
}
