namespace AUPS_Backend.DTO
{
    public class ObjectOfLaborTechnologicalProcedureDTO
    {
        public Guid ObjectOfLaborTechnologicalProcedureId { get; set; }

        public int OrderOfExecution { get; set; }

        public Guid ObjectOfLaborId { get; set; }

        public string? ObjectOfLaborName { get; set; }

        public Guid TechnologicalProcedureId { get; set; }

        public string? TechnologicalProcedureName { get; set; }

        public int TechnologicalProcedureDuration { get; set; }

        public string? TechnologicalSystemName { get; set; }

        public string? PlantName { get; set; }

        public Guid OrganizationalUnitId { get; set; }

        public string? OrganizationalUnitName { get; set; }

        public int TotalCount { get; set; }
    }
}
