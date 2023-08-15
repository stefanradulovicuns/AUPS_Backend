namespace AUPS_Backend.DTO
{
    public class TechnologicalProcedureUpdateDTO
    {
        public Guid TechnologicalProcedureId { get; set; }

        public string? TechnologicalProcedureName { get; set; }

        public int Duration { get; set; }

        public Guid OrganizationalUnitId { get; set; }

        public Guid PlantId { get; set; }

        public Guid TechnologicalSystemId { get; set; }
    }
}
