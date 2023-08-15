namespace AUPS_Backend.DTO
{
    public class TechnologicalProcedureCreateDTO
    {
        public string? TechnologicalProcedureName { get; set; }

        public int Duration { get; set; }

        public Guid OrganizationalUnitId { get; set; }

        public Guid PlantId { get; set; }

        public Guid TechnologicalSystemId { get; set; }
    }
}
